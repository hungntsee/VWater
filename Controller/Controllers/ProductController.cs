using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("GetActiveProduct")]
        public IActionResult GetActiveProduct()
        {
            var products = _productService.GetActiveProduct();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateModel model)
        {
            _productService.Create(model);
            return Ok(new { message = "Product created" });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductUpdateModel model)
        {
            _productService.Update(id, model);
            return Ok(new { message = "Product updated" });
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return Ok(new { message = "Product deleted" });
        }

        // GET api/<ProductController>/GetNumberOfArea
        [HttpGet("/GetNumberOfProduct")]
        public IActionResult GetNumberOfProduct()
        {
            return Ok(new { NumberOfProduct = _productService.GetNumberOfProduct() });
        }

        [HttpGet("/api/GetProductByProductType")]
        public IActionResult GetProductByProductType(int productType_id)
        {
            var products = _productService.GetProductByProductType(productType_id);
            return Ok(products);
        }

        [HttpGet("/api/ChangeProductActivation")]
        public IActionResult ChangeProductActivation(int id)
        {
            var product = _productService.ChangeProductActivation(id);
            return Ok(product);
        }
        
        private string _connectionString = "DefaultEndpointsProtocol=https;AccountName=vwaterblobstorage;AccountKey=VXEq91uZZ6FyWTSadQgEMFvUz6/gZedEezf0zKycEyCCsxm1OdCkd0YP7JuKYzdzv2azYBGTj0uH+AStRjoWcg==;EndpointSuffix=core.windows.net";
        [HttpPost("api/UploadProductImage")]
            public async Task<IActionResult> UploadProductImage(IFormFile files)
            {
                BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, "product");

                    using(var stream= new MemoryStream())
                    {
                        await files.CopyToAsync(stream);
                        stream.Position = 0;
                        await blobContainerClient.UploadBlobAsync(files.FileName, stream);
                    }
            string namePath= "https://vwaterblobstorage.blob.core.windows.net/product/" + files.FileName;
            return Ok(namePath);
            }

        [HttpGet("/api/SearchProductName")]
        public IActionResult SearchProductName(string search)
        {
            var products = _productService.SearchProductName(search);
            return Ok(products);
        }
    }
}
