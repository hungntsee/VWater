using AutoMapper;
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
        public Menu GetByTime(DateTime time);
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
            var menu = GetMenu(id);
            _context.Menus.Remove(menu);
            _context.SaveChangesAsync();
        }

        public IEnumerable<Menu> GetAll()
        {
            return _context.Menus.Include(a => a.Area).Include(a => a.ProductInMenus);
        }

        public Menu GetById(int id)
        {
            var menu = GetMenu(id);
            return menu;
        }

        public Menu GetByTime(DateTime time)
        {
            var menu = GetMenuByTime(time);
            return menu;
        }

        public void Update(int id, MenuUpdateModel model)
        {
            var menu = GetMenu(id);
            _mapper.Map(menu, model);
            _context.Menus.Update(menu);
            _context.SaveChangesAsync();
        }

        private Menu GetMenu(int id)
        {
            var menu = _context.Menus.Include(a => a.Area).Include(a => a.ProductInMenus).AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (menu == null) throw new KeyNotFoundException("Menu not found!");
            return menu;
        }

        private Menu GetMenuByTime(DateTime time)
        {
            var menus = _context.Menus;
            foreach (var menu in menus)
            {
                if (menu.ValidFrom < time && time < menu.ValidTo) return menu;
            }
            return null;
        }

        private void ValidateMenuDate(Menu menu)
        {
            if (menu == null) return;
            if (menu.ValidFrom > menu.ValidTo) throw new AppException("Date for ValidTo > ValidFrom!");          
            foreach(var m in _context.Menus)
            {
                if(menu.ValidFrom < m.ValidTo) throw new AppException("Date for ValidFrom must be newest.!");
            }
        }
    }
}
