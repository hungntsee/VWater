using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.ProductTypes
{
    public interface IProductTypeService
    {
        public IEnumerable<ProductType> GetAll();
        public ProductType GetById(int id);
        public void Create(ProductTypeCreateModel request);
        public void Update(int id, ProductTypeUpdateModel request);
        public void Delete(int id);
    }
    public class ProductTypeService : IProductTypeService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public ProductTypeService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<ProductType> GetAll()
        {
            return _context.ProductTypes.Include(a => a.Products);
        }

        public ProductType GetById(int id)
        {
            var productType = GetProductType(id);
            return productType;
        }

        public void Create(ProductTypeCreateModel request)
        {
            if (_context.ProductTypes.Any(t => t.ProductTypeName == request.ProductTypeName))
                throw new AppException("ProductType: '" + request.ProductTypeName + "' already exists");
            var productType = _mapper.Map<ProductType>(request);

            _context.ProductTypes.AddAsync(productType);
            _context.SaveChangesAsync();
        }

        public void Update(int id, ProductTypeUpdateModel request)
        {
            var productType = GetProductType(id);

            if (_context.ProductTypes.Any(t => t.ProductTypeName == request.ProductTypeName))
                throw new AppException("ProductType: '" + request.ProductTypeName + "' already exists");
            _mapper.Map(request, productType);
            _context.ProductTypes.Update(productType);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var productType = GetProductType(id);
            _context.ProductTypes.Remove(productType);
            _context.SaveChangesAsync();
        }

        private ProductType GetProductType(int id)
        {
            var productType = _context.ProductTypes.Find(id);
            if (productType == null) throw new KeyNotFoundException("ProductType not found!");
            return productType;
        }

    }
}
