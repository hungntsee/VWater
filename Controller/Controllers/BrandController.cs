using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Service.Brands;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // GET: api/<BrandController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = _brandService.GetAll();
            return Ok(brands);
        }

        // GET api/<BrandController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brand = _brandService.GetById(id);
            return Ok(brand);
        }

        // POST api/<BrandController>
        [HttpPost]
        public IActionResult Create([FromBody] BrandCreateModel request)
        {
            _brandService.Create(request);
            return Ok(new { message = "Brand created" });
        }

        // PUT api/<BrandController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BrandUpdateModel request)
        {
            _brandService.Update(id, request);
            return Ok(new { message = "Brand updated" });
        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _brandService.Delete(id);
            return Ok(new { message = "Brand deleted" });
        }

        private string _connectionString = "DefaultEndpointsProtocol=https;AccountName=vwaterblobstorage;AccountKey=VXEq91uZZ6FyWTSadQgEMFvUz6/gZedEezf0zKycEyCCsxm1OdCkd0YP7JuKYzdzv2azYBGTj0uH+AStRjoWcg==;EndpointSuffix=core.windows.net";
        [HttpPost("api/UploadBrandImage")]
        public async Task<IActionResult> UploadBrandImage(IFormFile files)
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, "brand");

            using (var stream = new MemoryStream())
            {
                await files.CopyToAsync(stream);
                stream.Position = 0;
                await blobContainerClient.UploadBlobAsync(files.FileName, stream);
            }
            string namePath = "https://vwaterblobstorage.blob.core.windows.net/brand/" + files.FileName;
            return Ok(namePath);
        }
    }
}
