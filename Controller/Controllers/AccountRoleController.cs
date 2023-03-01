using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private IAccountRoleService _accountRoleService;
        public AccountRoleController(IAccountRoleService accountRoleService)
        {
            _accountRoleService = accountRoleService;
        }

        // GET: api/<AccountRoleController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var stores = _accountRoleService.GetAll();
            return Ok(stores);
        }

        // GET api/<AccountRoleController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var store = _accountRoleService.GetById(id);
            return Ok(store);
        }

        // POST api/<AccountRoleController>
        [HttpPost]
        public IActionResult Create([FromBody] AccountRoleCreateModel request)
        {
            _accountRoleService.Create(request);
            return Ok(new { message = "Account Role created" });
        }

        // PUT api/<AccountRoleController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AccountRoleUpdateModel request)
        {
            _accountRoleService.Update(id, request);
            return Ok(new { message = "Account Role updated" });
        }

        // DELETE api/<AccountRoleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _accountRoleService.Delete(id);
            return Ok(new { message = "AccountRole deleted" });
        }
    }
}
