﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Data.Queries;
using VWater.Domain.Models;

namespace Service.Stores
{
    public interface IStoreService
    {
        public IEnumerable<Store> GetAll();
        public Store GetById(int id);
        public void Create(StoreCreateModel request);
        public void Update(int id, StoreUpdateModel request);
        public void Delete(int id);
        public int GetNumberOfStore();
    }
    public class StoreService : IStoreService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public StoreService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Store> GetAll()
        {
            return _context.Stores.Include(a => a.Area);
        }

        public Store GetById(int id)
        {
            var store = GetStore(id);
            return store;
        }

        public void Create(StoreCreateModel request)
        {
            if (_context.Stores.Any(b => b.StoreName == request.StoreName))
                throw new AppException("Store: '" + request.StoreName + "' already exists");
            var store = _mapper.Map<Store>(request);

            _context.Stores.AddAsync(store);
            _context.SaveChangesAsync();
        }

        public void Update(int id, StoreUpdateModel request)
        {
            var store = GetStore(id);

            _mapper.Map(request, store);
            _context.Stores.Update(store);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var store = GetStore(id);
            _context.Stores.Remove(store);
            _context.SaveChangesAsync();
        }

        private Store GetStore(int id)
        {
            var store = _context.Stores.Include(a => a.Area)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (store == null) throw new KeyNotFoundException("Store not found!");
            return store;
        }

        public int GetNumberOfStore()
        {
            var numberOfStore = _context.Stores.Count();

            return numberOfStore;
        }
    }
}
