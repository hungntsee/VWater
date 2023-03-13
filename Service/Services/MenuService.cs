using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IMenuService
    {
        public IEnumerable<Menu> GetAll();
        public Menu GetById(int id);
        public Menu GetMenu(DateTime time, int area_id);
        public IEnumerable<ProductFilterModel> FilterProductByType(string type, int menu_id);
        public void Create(MenuCreateModel model);
        public void Update(int id, MenuUpdateModel model);
        public void Delete(int id);
    }
    public class MenuService : IMenuService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public MenuService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(MenuCreateModel model)
        {
            var menu = _mapper.Map<Menu>(model);
            ValidateMenuDate(menu);
            _context.Menus.Add(menu);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var menu = GetMenuById(id);
            _context.Menus.Remove(menu);
            _context.SaveChangesAsync();
        }

        public IEnumerable<Menu> GetAll()
        {
            return _context.Menus.Include(a => a.ProductInMenus).IgnoreAutoIncludes();
        }

        public Menu GetById(int id)
        {
            var menu = GetMenuById(id);
            return menu;
        }

        public Menu GetMenu(DateTime time, int area_id)
        {
            var menu = GetMenuByArea(time, area_id);
            return menu;
        }

        public void Update(int id, MenuUpdateModel model)
        {
            var menu = GetMenuById(id);
            _mapper.Map(menu, model);
            _context.Menus.Update(menu);
            _context.SaveChangesAsync();
        }

        private Menu GetMenuById(int id)
        {
            var menu = _context.Menus.Include(a => a.Area).Include(a => a.ProductInMenus).AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (menu == null) throw new KeyNotFoundException("Menu not found!");
            return menu;
        }

        private Menu GetMenuByArea(DateTime time, int area_id)
        {
            var menu = _context.Menus.Include(a => a.ProductInMenus)
                .AsNoTracking().FirstOrDefault(a => a.AreaId == area_id && a.ValidFrom <= time && time <= a.ValidTo);
            if (menu == null) return _context.Menus.Include(a => a.ProductInMenus).Last();
            
            return menu;
            
        }

        private void ValidateMenuDate(Menu menu)
        {
            if (menu == null) return;
            if (menu.ValidFrom > menu.ValidTo) throw new AppException("Date for ValidTo > ValidFrom!");
            foreach (var m in _context.Menus)
            {
                if (menu.ValidFrom < m.ValidTo) throw new AppException("Date for ValidFrom must be newest.!");
            }
        }

        public IEnumerable<ProductFilterModel> FilterProductByType(string type, int menu_id)
        {
            var list = FilterProduct(type, menu_id);
            if (list == null) throw new AppException("This type don't have any product");
            return list;
        }

        private IEnumerable<ProductFilterModel> FilterProduct (string type, int menu_id)
        {
            var menu = GetMenuById(menu_id);
            var productInMenus= menu.ProductInMenus;
            var productList = new List<ProductFilterModel>();
            foreach (var product in productInMenus)
            {
                if(type.ToLower() == product.Product.ProductType.ProductTypeName.ToLower())
                {
                    productList.Add(_mapper.Map<ProductFilterModel>(product));
                }
            }

            return productList;
        }
    }
}
