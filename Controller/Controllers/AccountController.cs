using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Domain.Models;
using Service.Account;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IConfiguration _config;
        public AccountController(IAccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            _config = config;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountService.GetAll();
            return Ok(accounts);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var account = _accountService.GetById(id);
            return Ok(account);
        }

        // POST api/<AccountController>
        [HttpPost("register")]
        public IActionResult Create(AccountCreateModel request)
        {
            _accountService.Create(request);
            return Ok(new { message = "User created" });
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, AccountUpdateModel request)
        {
            _accountService.Update(id, request);
            return Ok(new { message = "User update" });
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _accountService.Delete(id);
            return Ok(new { message = "User deleted" });
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var account = _accountService.Login(request);
            return Ok(new
            {
                message = "Login Success",
                accessToken = account.AccessToken
            });
        }

        [HttpPost("access_token")]
        public IActionResult LoginByToken(string token)
        {
            var account = _accountService.LoginByToken(token);
            return Ok(new { message = "Login Success\n" + account });
        }
    }
}
