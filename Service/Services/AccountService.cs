namespace Service.Account;

using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Domain.Models;
using Service.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

public interface IAccountService
{
    public IEnumerable<Account> GetAll();
    public Account GetById(int id);
    public void Create(AccountCreateModel request);
    public Shipper CreateForShipper(ShipperCreateModel request);
    public void Update(int id, AccountUpdateModel request);
    public void Delete(int id);
    public Account Login(LoginRequest request);
    public Account LoginByToken(string token);
}
public class AccountService : IAccountService
{
    private VWaterContext _context;
    private readonly IMapper _mapper;
    private readonly AppSetting _appSettings;
    private readonly IConfiguration _configuration;

    public AccountService(VWaterContext context, IMapper mapper, IOptions<AppSetting> appSettings, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _appSettings = appSettings.Value;
        _configuration = configuration;
    }
    public IEnumerable<Account> GetAll()
    {

        var accounts = _context.Accounts.Include(a => a.RoleAccountRole);
        return accounts;
    }

    public Account GetById(int id)
    {
        var accountRespone = GetAccount(id);
        return accountRespone;
    }

    public void Create(AccountCreateModel request)
    {
        if (_context.Accounts.AnyAsync(a => a.Email == request.Email).Result)
            throw new AppException("User with the email '" + request.Email + "' already exists");
        var account = _mapper.Map<Account>(request);
        account.Username = request.Email;
        /*account.Password = BCrypt.HashPassword(request.Password);*/

        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public void Update(int id, AccountUpdateModel request)
    {
        var account = GetAccount(id);

        if (request.Email != account.Email && _context.Accounts.Any(a => a.Email == request.Email))
            throw new AppException("User with the email '" + request.Email + "' already exists");

        _mapper.Map(request, account);

        if (!string.IsNullOrEmpty(request.Email))
            account.Username = request.Email.Trim();

        _context.Accounts.Update(account);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var account = GetAccount(id);
        _context.Accounts.Remove(account);
        _context.SaveChanges();
    }

    private Account GetAccount(int id)
    {
        var account = _context.Accounts.Include(a => a.RoleAccountRole).AsNoTracking().FirstOrDefault(a => a.Id == id);
        if (account == null) throw new KeyNotFoundException("Account not found");
        return account;
    }

    public Account Login(LoginRequest request)
    {
        var account = _context.Accounts.Include(a => a.RoleAccountRole).FirstOrDefaultAsync(x => x.Username.ToLower() == request.Email.ToLower()).Result;

        if (account != null && account.Password == request.Password)
        {
            var role = account.RoleAccountRole.RoleName;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = GetToken(authClaims);
            account.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
        }

        else throw new AppException("Login Fail");

        /*account.AccessToken = generateJwtToken(account);*/
        _context.Accounts.Update(account);
        _context.SaveChanges();

        return account;
    }

    public Account LoginByToken(string token)
    {
        var account = _context.Accounts.SingleOrDefaultAsync(x => x.AccessToken == token).Result;
        return account;
    }

    private string generateJwtToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken GetToken(List<Claim> claims)
    {

        var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public Shipper CreateForShipper(ShipperCreateModel request)
    {
        if (_context.Accounts.AnyAsync(a => a.Email == request.AccountCreateModel.Email).Result)
            throw new AppException("User with the email '" + request.AccountCreateModel.Email + "' already exists");
        var account = _mapper.Map<Account>(request.AccountCreateModel);
        account.Username = request.AccountCreateModel.Email;

        _context.Accounts.Add(account);
        _context.SaveChanges();
        var shipper = new Shipper();
        if(account.RoleId == 6)
        {
            shipper = _mapper.Map<Shipper>(request);
            shipper.AccountId = account.Id;
            shipper.Fullname = account.FirstName +" "+ account.FirstName;
            _context.Shippers.Add(shipper);
            _context.SaveChanges();

            var wallet = new Wallet();
            wallet.ShipperId = shipper.Id;
            wallet.Credit = 0;
            _context.Wallets.Add(wallet);
            _context.SaveChanges();

            shipper.Wallet = wallet;
        }
        shipper.Account = account;
        shipper.Orders = null;

        return shipper;
    }
}

