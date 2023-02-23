﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IMenuService
    {
        public IEnumerable<Menu> GetAll();
        public Menu GetById(int id);
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
    }
}
