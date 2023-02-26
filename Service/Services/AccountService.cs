namespace Service.Account;

using AutoMapper;
using BCrypt.Net;
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
    public void Update(int id, AccountUpdateModel request);
    public void Delete(int id);
    public Account Login(LoginRequest request);
    public Account LoginByToken(string token);
}
public class AccountService : IAccountService
{
    private VWaterContext _context;
    private readonly IMapper _mapper;
    private readonly AppSetting _appSettings ;
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
        if (_context.Accounts.AnyAsync(a => a.Username == request.Username).Result)
            throw new AppException("User with the username '" + request.Username + "' already exists");
        var account = _mapper.Map<Account>(request);

        /*account.Password = BCrypt.HashPassword(request.Password);*/

        _context.Accounts.AddAsync(account);
        _context.SaveChangesAsync();
    }

    public void Update(int id, AccountUpdateModel request)
    {
        var account = GetAccount(id);

        if (request.Username != account.Email && _context.Accounts.Any(a => a.Username == request.Username))
            throw new AppException("User with the username '" + request.Username + "' already exists");
        if (!string.IsNullOrEmpty(request.Password))
            account.Password = BCrypt.HashPassword(request.Password);

        _mapper.Map(request, account);
        _context.Accounts.Update(account);
        _context.SaveChangesAsync();
    }

    public void Delete(int id)
    {
        var account = GetAccount(id);
        _context.Accounts.Remove(account);
        _context.SaveChangesAsync();
    }

    private Account GetAccount(int id)
    {
        var account = _context.Accounts.Include(a => a.RoleAccountRole).AsNoTracking().FirstOrDefault(a => a.Id == id);
        if (account == null) throw new KeyNotFoundException("Account not found");
        return account;
    }

    public Account Login(LoginRequest request)
    {
        var account = _context.Accounts.Include(a => a.RoleAccountRole).FirstOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower()).Result;

        if(account != null && account.Password == request.Password)
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
        _context.SaveChangesAsync();

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

    private JwtSecurityToken GetToken(List<Claim> claims) {
        
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
}

