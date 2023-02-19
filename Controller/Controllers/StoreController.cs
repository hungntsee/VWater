﻿using Microsoft.AspNetCore.Mvc;
using Service.Stores;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: api/<StoreController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var stores = _storeService.GetAll();
            return Ok(stores);
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var store = _storeService.GetById(id);
            return Ok(store);
        }

        // POST api/<StoreController>
        [HttpPost("register")]
        public IActionResult Create(StoreCreateModel request)
        {
            _storeService.Create(request);
            return Ok(new { message = "Store created" });
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, StoreUpdateModel request)
        {
            _storeService.Update(id, request);
            return Ok(new { message = "Store updated" });
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _storeService.Delete(id);
            return Ok(new { message = "Store deleted" });
        }
    }
}
