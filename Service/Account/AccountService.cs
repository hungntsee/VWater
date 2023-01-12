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
using System.Data.Entity;
using MapsterMapper;

public interface IAccountService
{
    public IEnumerable<Account> GetAll();
    public AccountRespone GetById(int id);
    public void Create(AccountRequest request);
    public void Update(int id, AccountUpdateRequest request);
    public void Delete(int id);
    public Account Login(LoginRequest request);
    public Account LoginByToken(string token);
}
public class AccountService: IAccountService
{
    private DBContext _context;
    private readonly IMapper _mapper;
    private readonly AppSetting _appSetting;

    public AccountService(DBContext context, IOptions<AppSetting> appSetting, IMapper mapper)
    {
        _context = context;
        _appSetting = appSetting.Value;
        _mapper = mapper;
    }
    public IEnumerable<Account> GetAll()
    {
        /*var accountsRespone =_mapper.Map<IEnumerable<AccountRespone>>(_context.Account);*/
        return _context.Account;
    }

    public AccountRespone GetById(int id)
    {
        var accountRespone = _mapper.Map<AccountRespone>(GetAccount(id));
        return accountRespone;
    }

    public void Create(AccountRequest request)
    {
        if (_context.Account.AnyAsync(a => a.Username == request.Username).Result) 
            throw new AppException("User with the username '" + request.Username + "' already exists");
        var account = _mapper.Map<Account>(request);

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

    public Account Login(LoginRequest request)
    {
        var account = _context.Account.SingleOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password).Result;

        if (account == null) throw new AppException("Login Fail");

        account.Access_token = generateJwtToken(account);
        _context.Account.Update(account);
        _context.SaveChangesAsync();

        return account;
    }

    public Account LoginByToken(string token)
    {
        var account = _context.Account.SingleOrDefaultAsync(x => x.Access_token == token).Result;
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

