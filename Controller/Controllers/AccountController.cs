﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using Repository.Model.Account;
using Service.Account;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
        [HttpPost]
        public IActionResult Create(AccountRequest request)
        {
            _accountService.Create(request);
            return Ok(new {message = "User created"});
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, AccountUpdateRequest request)
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

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            var response = _accountService.Login(request);
            return Ok( new { message = "Login Success/n" + response});
        }
    }
}
