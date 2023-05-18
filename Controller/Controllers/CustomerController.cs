using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _customerService.GetById(id);
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Create([FromBody] CustomerCreateModel request)
        {
            var customer = _customerService.Create(request);
            if (customer.Note == "Welcome Back")
            {
                return Ok(new
                {
                    message = "Welcome Back",
                    customer
                });
            }
            return Ok(new
            {
                message = "Customer created",
                customer
            });
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerUpdateModel request)
        {
            _customerService.Update(id, request);
            return Ok(new { message = "Customer updated" });
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _customerService.Delete(id);
            return Ok(new { message = "Customer deleted" });
        }

        [HttpGet("/api/getHistoryOrder")]
        public IActionResult GetHistoryOrder(int customer_id)
        {
            var customer = _customerService.GetHistoryOrder(customer_id);
            return Ok(customer);
        }

        [HttpGet("/api/GetReportPerCustomer")]
        public IActionResult GetReportPerCustomer(int customer_id)
        {
            var report = _customerService.GetReportPerCustomer(customer_id);
            return Ok(report);
        }
    }
}
