using AutoMapper;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetAll();
        public ProductReadModel GetById(int id);
        public void Create(ProductCreateModel model);
        public void Update(int id, ProductUpdateModel model);
        public void Delete(int id);

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

        public ProductReadModel GetById(int id)
        {
            var productResponse = _mapper.Map<ProductReadModel>(GetProduct(id));
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
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) throw new KeyNotFoundException("Product not found!");
            return product;
        }
    }
}
