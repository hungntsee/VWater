﻿using Microsoft.AspNetCore.Mvc;
using Service.Account;
using Service.Good;
using VWater.Data.Entities;
using VWater.Domain.Models;
using Service.GoodsCompositions;
using Repository.Domain.Models;
using Service.GoodsInBaselines;

namespace Controller.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GoodsInBaselineController : ControllerBase
    {
        private IGoodsInBaselineService _goodsInBaselineService;
        public GoodsInBaselineController(IGoodsInBaselineService goodsInBaselineService)
        {
            _goodsInBaselineService = goodsInBaselineService;
        }

        // GET: api/<GoodsInBaselineController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var goodsInBaselines = _goodsInBaselineService.GetAll();
            return Ok(goodsInBaselines);
        }

        // GET api/<GoodsInBaselineController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var goodsInBaselines = _goodsInBaselineService.GetById(id);
            return Ok(goodsInBaselines);
        }

        // POST api/<GoodsInBaselineController>
        [HttpPost("register")]
        public IActionResult Create(GoodsInBaselineCreateModel request)
        {
            _goodsInBaselineService.Create(request);
            return Ok(new { message = "Goods In Baseline created" });
        }

        // PUT api/<GoodsInBaselineController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, GoodsInBaselineUpdateModel request)
        {
            _goodsInBaselineService.Update(id, request);
            return Ok(new { message = "Goods In Baseline updated" });
        }

        // DELETE api/<GoodsInBaselineController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _goodsInBaselineService.Delete(id);
            return Ok(new { message = "Goods In Baseline deleted" });
        }
    }
}