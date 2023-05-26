using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IMenuService
    {
        public IEnumerable<Menu> GetAll();
        public Menu GetById(int id);
        public Menu GetMenu(DateTime time, int store_id);
        public IEnumerable<ProductInMenu> FilterProductByType(int type_id, int menu_id);
        public void Create(MenuCreateModel model);
        public void Update(int id, MenuUpdateModel model);
        public void Delete(int id);
        public List<Menu> GetMenuByStoreId(int area_id);
        public ICollection<ProductReadModel> GetMenuForStore(int store_id);
        //public Menu GetMenuByStore(int store_id);
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

            //menu.ValidFrom.ToFileTimeUtc();
            //menu.ValidTo.ToFileTimeUtc();

            ValidateMenuDate(menu);
            _context.Menus.Add(menu);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var menu = GetMenuById(id);
            _context.Menus.Remove(menu);
            _context.SaveChanges();
        }

        public IEnumerable<Menu> GetAll()
        {
            return _context.Menus.Include(a => a.ProductInMenus).IgnoreAutoIncludes().OrderByDescending(a => a.ValidTo);
        }

        public Menu GetById(int id)
        {
            var menu = GetMenuById(id);
            return menu;
        }

        public Menu GetMenu(DateTime time, int store_id)
        {
            var menu = GetMenuByArea(time, store_id);
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
            var menu = _context.Menus.Include(a => a.Store).Include(a => a.ProductInMenus)
                .ThenInclude(a => a.Product).ThenInclude(a=>a.ProductType).AsNoTracking().FirstOrDefault(p => p.Id == id);
            menu.Store.Menus = null;  

            if (menu == null) throw new KeyNotFoundException("Menu not found!");
            return menu;
        }

        private Menu GetMenuByArea(DateTime time, int store_id)
        {
            var menu = _context.Menus.Include(a => a.ProductInMenus).ThenInclude(a => a.Product).Include(a => a.Store)
                .AsNoTracking().FirstOrDefault(a => a.StoreId == store_id && a.ValidFrom <= time && time <= a.ValidTo);
            if (menu == null) return _context.Menus.Include(a => a.ProductInMenus).Last();

            foreach(var productInMenu in menu.ProductInMenus)
            {
                productInMenu.Product.ProductInMenus = null;
            }
            
            return menu;
            
        }

        private void ValidateMenuDate(Menu menu)
        {
            if (menu == null) return;
            if (menu.ValidFrom > menu.ValidTo) throw new AppException("Date for ValidTo > ValidFrom!");
            foreach (var m in _context.Menus)
            {
                if (menu.ValidFrom < m.ValidTo && menu.StoreId == m.StoreId)
                {
                    throw new AppException("Date for ValidFrom must be newest.!");
                }
            }
        }

        public IEnumerable<ProductInMenu> FilterProductByType(int type_id, int menu_id)
        {
            var list = FilterProduct(type_id, menu_id);
            if (list == null) throw new AppException("This type don't have any product");
            return list;
        }

        private IEnumerable<ProductInMenu> FilterProduct (int type_id, int menu_id)
        {         
            var productInMenus= ProductInMenuExtensions.ByMenuId(_context.ProductInMenus.Include(a => a.Product),menu_id);
            var productList = new List<ProductInMenu>();
            foreach (var product in productInMenus)
            {
                product.Product.ProductInMenus = null;
                if(type_id == product.Product.ProductType_Id)
                {
                    productList.Add(product);
                }
            }
            return productList;
        }

        public List<Menu> GetMenuByStoreId(int store_id)
        {
            var menus = MenuExtensions.ByStoreId(_context.Menus
                .Include(a=>a.Store).OrderByDescending(a=>a.ValidTo), store_id);

            return menus.ToList();
        }

        public ICollection<ProductReadModel> GetMenuForStore(int store_id)
        {  
            var store = _context.Stores.AsNoTracking().FirstOrDefault(a => a.Id == store_id);
            var time = DateTime.UtcNow.AddHours(7);

            var menu = _context.Menus.Include(a => a.ProductInMenus).ThenInclude(a => a.Product)
                 .AsNoTracking().FirstOrDefault(a => a.StoreId == store_id && a.ValidFrom <= time && time <= a.ValidTo);
            if (menu == null) menu = _context.Menus.Include(a => a.ProductInMenus).ThenInclude(a => a.Product).Last();

            ProductReadModel model = new ProductReadModel();
            ICollection<ProductReadModel> list = null;

            foreach (var product in menu.ProductInMenus)
            {
                model = _mapper.Map<ProductReadModel>(product.Product);
                //model.ProductsInBaseline = product.Product.ProductsInBaselines.Last();
                list.Add(model);
            }
            return list;
        }

        /*
        public Menu GetMenuByStore(int store_id)
        {
            var store = _context.Stores.AsNoTracking().FirstOrDefault( a=> a.Id == store_id);
            var time = DateTime.UtcNow.AddHours(7);

            return GetMenuByArea(time, store.AreaId);
        }       
        */
    }
}
