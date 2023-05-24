using Microsoft.AspNetCore.Mvc;
using Repository.Domain.Models;
using Service.Transactions;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/<TransactionController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var transactions = _transactionService.GetAll();
            return Ok(transactions);
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var transaction = _transactionService.GetById(id);
            return Ok(transaction);
        }

        // POST api/<TransactionController>
        [HttpPost]
        public IActionResult Create([FromBody] TransactionCreateModel request)
        {
            var list = _transactionService.Create(request);
            return Ok(list);
        }

        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TransactionUpdateModel request)
        {
            _transactionService.Update(id, request);
            return Ok(new { message = "Transaction updated" });
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _transactionService.Delete(id);
            return Ok(new { message = "Transaction deleted" });
        }
    }
}
