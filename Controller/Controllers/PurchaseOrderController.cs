using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private IPurchaseOrderService _purchasePurchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService purchasePurchaseOrderService)
        {
            _purchasePurchaseOrderService = purchasePurchaseOrderService;
        }

        // GET: api/<PurchaseOrderController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _purchasePurchaseOrderService.GetAll();
            return Ok(products);
        }

        // GET api/<PurchaseOrderController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _purchasePurchaseOrderService.GetById(id);
            return Ok(product);
        }

        // POST api/<PurchaseOrderController>
        [HttpPost]
        public IActionResult Create([FromBody] PurchaseOrderCreateModel model)
        {
            _purchasePurchaseOrderService.Create(model);
            return Ok(new { message = "PurchaseOrder created" });
        }

        // PUT api/<PurchaseOrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PurchaseOrderUpdateModel model)
        {
            _purchasePurchaseOrderService.Update(id, model);
            return Ok(new { message = "PurchaseOrder updated" });
        }

        // DELETE api/<PurchaseOrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _purchasePurchaseOrderService.Delete(id);
            return Ok(new { message = "PurchaseOrder deleted" });
        }
    }
}
