using Microsoft.AspNetCore.Mvc;
using Service.TransactionTypes;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {
        private ITransactionTypeService _transactionTypeService;
        public TransactionTypeController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        // GET: api/<TransactionTypeController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var transactionTypes = _transactionTypeService.GetAll();
            return Ok(transactionTypes);
        }

        // GET api/<TransactionTypeController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var transactionType = _transactionTypeService.GetById(id);
            return Ok(transactionType);
        }

        // POST api/<TransactionTypeController>
        [HttpPost]
        public IActionResult Create([FromBody] TransactionTypeCreateModel request)
        {
            _transactionTypeService.Create(request);
            return Ok(new { message = "TransactionType created" });
        }

        // PUT api/<TransactionTypeController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TransactionTypeUpdateModel request)
        {
            _transactionTypeService.Update(id, request);
            return Ok(new { message = "TransactionType updated" });
        }

        // DELETE api/<TransactionTypeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _transactionTypeService.Delete(id);
            return Ok(new { message = "TransactionType deleted" });
        }
    }
}
