using Microsoft.AspNetCore.Mvc;
using Service.Quotations;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private IQuotationService _quotationService;
        public QuotationController(IQuotationService quotationService)
        {
            _quotationService = quotationService;
        }

        // GET: api/<QuotationController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var quotations = _quotationService.GetAll();
            return Ok(quotations);
        }

        // GET api/<QuotationController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var quotation = _quotationService.GetById(id);
            return Ok(quotation);
        }

        // POST api/<QuotationController>
        [HttpPost("register")]
        public IActionResult Create(QuotationCreateModel request)
        {
            _quotationService.Create(request);
            return Ok(new { message = "Quotation created" });
        }

        // PUT api/<QuotationController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, QuotationUpdateModel request)
        {
            _quotationService.Update(id, request);
            return Ok(new { message = "Quotation updated" });
        }

        // DELETE api/<QuotationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _quotationService.Delete(id);
            return Ok(new { message = "Quotation deleted" });
        }
    }
}
