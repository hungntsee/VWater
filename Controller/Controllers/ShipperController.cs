﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetNumberOfShipper")]
        public IActionResult GetNumberOfShipper()
        {
            return Ok(new { numberOfShipper = _shipperService.GetNumberOfShipper() });
        }
    }
}