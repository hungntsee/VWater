using Microsoft.AspNetCore.Mvc;
using Repository.Domain.Models;
using Service.Shippers;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private IShipperService _shipperService;
        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        // GET: api/<ShipperController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var shippers = _shipperService.GetAll();
            return Ok(shippers);
        }

        // GET api/<ShipperController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var shipper = _shipperService.GetById(id);
            return Ok(shipper);
        }

        // PUT api/<ShipperController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ShipperUpdateModel request)
        {
            _shipperService.Update(id, request);
            return Ok(new { message = "Shipper updated" });
        }

        [HttpGet("/api/GetNumberOfShipper")]
        public IActionResult GetNumberOfShipper()
        {
            return Ok(new { numberOfShipper = _shipperService.GetNumberOfShipper() });
        }

        /*
        [HttpPut("StatusOfShipper")]
        public IActionResult StatusOfShipper(int id, [FromBody] ShipperStatusModel request1)
        {
            _shipperService.StatusOfShipper(id, request1);
            return Ok(new { message = "Shipper status changed" });
        }
        */

        [HttpGet("/api/GetReportForShipper")]
        public IActionResult GetReportForShipper(int shipper_id)
        {
            return Ok(_shipperService.GetReportForShipper(shipper_id));
        }

        [HttpGet("/api/GetShipperByStoreId")]
        public IActionResult GetShipperByStoreId(int store_id)
        {
            var shippers = _shipperService.GetShipperByStoreId(store_id);
            return Ok(shippers);
        }

        [HttpGet("/api/ChangeStatus")]
        public IActionResult ChangeStatus(int id)
        {
            var shipper = _shipperService.ChangeStatus(id);
            return Ok(shipper);
        }
    }
}
