namespace Service.Account;

using BCrypt.Net;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Entity;
using Repository.Model.Account;
using Service.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

public interface IAccountService
{
    IEnumerable<Account> GetAll();
    Account GetById(int id);
    void Create(AccountRequest request);
    void Update(int id, AccountUpdateRequest request);
    void Delete(int id);
    void Login(LoginRequest request);
}
public class AccountService: IAccountService
{
    private DBContext _context;
    private readonly AppSetting _appSetting;

    public AccountService(DBContext context, IOptions<AppSetting> appSetting)
    {
        _context = context;
        _appSetting = appSetting.Value;
    }
    public IEnumerable<Account> GetAll()
    {
        return _context.Account;
    }

    public Account GetById(int id)
    {
        return GetAccount(id);
    }

    public void Create(AccountRequest request)
    {
        if (_context.Account.AnyAsync(a => a.Username == request.Username).Result) 
            throw new AppException("User with the username '" + request.Username + "' already exists");
        var account = request.Adapt<Account>();

        account.Password = BCrypt.HashPassword(request.Password);

        _context.Account.AddAsync(account);
        _context.SaveChangesAsync();
    }

    public void Update(int id, AccountUpdateRequest request)
    {
        var account = GetAccount(id);

        if (request.Username != account.Email && _context.Account.Any(a => a.Username == request.Username))
            throw new AppException("User with the username '" + request.Username + "' already exists");
        if (!string.IsNullOrEmpty(request.Password))
            account.Password = BCrypt.HashPassword(request.Password);

        account.Adapt(request);
        _context.Account.Update(account);
        _context.SaveChangesAsync();
    }

    public void Delete(int id)
    {
        var account = GetAccount(id);
        _context.Account.Remove(account);
        _context.SaveChangesAsync();
    }

    private Account GetAccount(int id)
    {
        var account = _context.Account.Find(id);
        if (account == null) throw new KeyNotFoundException("Account not found");
        return account;
    }

    public void Login(LoginRequest request)
    {
        var account = _context.Account.SingleOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password).Result;

        if (account == null) throw new AppException("Login Fail");

        account.Access_token = generateJwtToken(account);
        _context.Account.Update(account);
        _context.SaveChangesAsync();
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

