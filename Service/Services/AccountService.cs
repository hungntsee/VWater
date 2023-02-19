namespace Service.Account;

using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
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
    public AccountReadModel GetById(int id);
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
    private readonly IConfiguration _config;

    public AccountService(VWaterContext context, IMapper mapper, IConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
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
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,account.Username),
            new Claim(ClaimTypes.Email,account.Email),
            new Claim(ClaimTypes.GivenName, account.LastName),
            new Claim(ClaimTypes.Surname, account.FirstName),
            new Claim(ClaimTypes.Role,account.RoleAccountRole.RoleName),
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims
            , expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

