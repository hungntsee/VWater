using Microsoft.AspNetCore.Mvc;
using Repository.Domain.Models;
using Service.Wallets;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        // GET: api/<WalletController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var wallets = _walletService.GetAll();
            return Ok(wallets);
        }

        // GET api/<WalletController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var wallet = _walletService.GetById(id);
            return Ok(wallet);
        }

        // POST api/<WalletController>
        [HttpPost]
        public IActionResult Create([FromBody] WalletCreateModel request)
        {
            _walletService.Create(request);
            return Ok(new { message = "Wallet created" });
        }

        // PUT api/<WalletController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WalletUpdateModel request)
        {
            _walletService.Update(id, request);
            return Ok(new { message = "Wallet updated" });
        }

        // DELETE api/<WalletController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _walletService.Delete(id);
            return Ok(new { message = "Wallet deleted" });
        }

        [HttpGet("/api/GetWalletByShipperId")]
        public IActionResult GetWalletByShipperId(int shipper_id)
        {
            var wallets = _walletService.GetWalletByShipperId(shipper_id);
            return Ok(wallets);
        }
    }
}
