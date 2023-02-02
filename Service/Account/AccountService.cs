namespace Service.Account;

using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using VWater.Data;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;
using Repository.Domain.Models;

public interface IAccountService
{
    public IEnumerable<Account> GetAll();
    public AccountReadModel GetById(int id);
    public void Create(AccountCreateModel request);
    public void Update(int id, AccountUpdateModel request);
    public void Delete(int id);
    public Account Login(LoginRequest request);
    public Account LoginByToken(string token);
}
public class AccountService: IAccountService
{
    private VWaterContext _context;
    private readonly IMapper _mapper;
    private readonly AppSetting _appSetting;

    public AccountService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
    {
        _context = context;
        _appSetting = appSetting.Value;
        _mapper = mapper;
    }
    public IEnumerable<Account> GetAll()
    {
        /*var accountsRespone =_mapper.Map<IEnumerable<AccountRespone>>(_context.Account);*/
        return _context.Accounts;
    }

    public AccountReadModel GetById(int id)
    {
        var accountRespone = _mapper.Map<AccountReadModel>(GetAccount(id));
        return accountRespone;
    }

    public void Create(AccountCreateModel request)
    {
        if (_context.Accounts.AnyAsync(a => a.Username == request.Username).Result) 
            throw new AppException("User with the username '" + request.Username + "' already exists");
        var account = _mapper.Map<Account>(request);

        account.Password = BCrypt.HashPassword(request.Password);

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

        _mapper.Map(request,account);
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
        var account = _context.Accounts.Find(id);
        if (account == null) throw new KeyNotFoundException("Account not found");
        return account;
    }

    public Account Login(LoginRequest request)
    {
        var account = _context.Accounts.SingleOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password).Result;

        if (account == null) throw new AppException("Login Fail");

        account.AccessToken = generateJwtToken(account);
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
        var tokenHadler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
        var tokenDescriptop = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHadler.CreateToken(tokenDescriptop);
        return tokenHadler.WriteToken(token);
    }
}

