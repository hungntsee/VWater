using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IProductInMenuService
    {
        public IEnumerable<ProductInMenu> GetAll();
        public ProductInMenu GetById(int id);
        public void Create(ProductInMenuCreateModel model);
        public void Update(int id, ProductInMenuUpdateModel model);
        public void Delete(int id);
    }
    public class ProductInMenuService : IProductInMenuService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        public ProductInMenuService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ProductInMenu> GetAll()
        {
            return _context.ProductInMenus;
        }

        public ProductInMenu GetById(int id)
        {
            var productInMenu = GetProductInMenu(id);
            return productInMenu;
        }

        public void Create(ProductInMenuCreateModel model)
        {
            var productInMenu = _mapper.Map<ProductInMenu>(model);

            _context.ProductInMenus.AddAsync(productInMenu);
            _context.SaveChangesAsync();
        }

        public void Update(int id, ProductInMenuUpdateModel model)
        {
            var productInMenu = GetProductInMenu(id);
            _mapper.Map(model, productInMenu);
            _context.ProductInMenus.Update(productInMenu);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var productInMenu = GetProductInMenu(id);
            _context.ProductInMenus.Remove(productInMenu);
            _context.SaveChangesAsync();
        }

        private ProductInMenu GetProductInMenu(int id)
        {
            var productInMenu = _context.ProductInMenus.Include(a => a.Menu).Include(a => a.Product)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (productInMenu == null) throw new KeyNotFoundException("Product In Menu not found!");
            return productInMenu;
        }
    }
}
