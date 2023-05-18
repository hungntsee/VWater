using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetAll();
        public Product GetById(int id);
        public void Create(ProductCreateModel model);
        public void Update(int id, ProductUpdateModel model);
        public void Delete(int id);
        public int GetNumberOfProduct();
        public List<Product> GetProductByProductType(int productType_id);
        public Product ChangeProductActivation(int id);
        public IEnumerable<Product> GetActiveProduct();
    }
    public class ProductService : IProductService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public ProductService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public IEnumerable<Product> GetActiveProduct()
        {
            return _context.Products.Where(a => a.IsActive == true);
        }

        public Product GetById(int id)
        {
            var productResponse = GetProduct(id);
            return productResponse;
        }

        public void Create(ProductCreateModel model)
        {
            var product = _mapper.Map<Product>(model);
            _context.Products.AddAsync(product);
            _context.SaveChangesAsync();
        }

        public void Update(int id, ProductUpdateModel model)
        {
            var product = GetProduct(id);
            _mapper.Map(model, product);
            _context.Products.Update(product);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var product = GetProduct(id);
            _context.Products.Remove(product);
            _context.SaveChangesAsync();
        }

        private Product GetProduct(int id)
        {
            var product = _context.Products
                .AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (product == null) throw new KeyNotFoundException("Product not found!");
            return product;
        }

        public int GetNumberOfProduct()
        {
            return _context.Products.Count();   
        }

        public List<Product> GetProductByProductType(int productType_id)
        {
            var product = ProductExtensions.ByProductTypeId(
                _context.Products,
                productType_id);

            return product.ToList();
        }

        public Product ChangeProductActivation(int id)
        {
            var product = GetProduct(id);

            if (product.IsActive == true) { product.IsActive = false; }
            else { product.IsActive = true; }
            _context.Products.Update(product);
            _context.SaveChangesAsync();

            return product;
        }

    }

}
