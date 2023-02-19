﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Warehouses
{
    public interface IWarehouseService
    {
        public IEnumerable<Warehouse> GetAll();
        public WarehouseReadModel GetById(int id);
        public void Create(WarehouseCreateModel request);
        public void Update(int id, WarehouseUpdateModel request);
        public void Delete(int id);
    }
    public class WarehouseService : IWarehouseService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public WarehouseService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<Warehouse> GetAll()
        {
            return _context.Warehouses;
        }

        public WarehouseReadModel GetById(int id)
        {
            var warehouse = _mapper.Map<WarehouseReadModel>(GetWarehouse(id));
            return warehouse;
        }

        public void Create(WarehouseCreateModel request)
        {
            var warehouse = _mapper.Map<Warehouse>(request);

            _context.Warehouses.AddAsync(warehouse);
            _context.SaveChangesAsync();
        }

        public void Update(int id, WarehouseUpdateModel request)
        {
            var warehouse = GetWarehouse(id);

            _mapper.Map(request, warehouse);
            _context.Warehouses.Update(warehouse);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var warehouse = GetWarehouse(id);
            _context.Warehouses.Remove(warehouse);
            _context.SaveChangesAsync();
        }

        private Warehouse GetWarehouse(int id)
        {
            var warehouse = _context.Warehouses.Find(id);
            if (warehouse == null) throw new KeyNotFoundException("Warehouse not found!");
            return warehouse;
        }

    }
}
