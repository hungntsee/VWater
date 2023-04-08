using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Warehouses
{
    public interface IWarehouseService
    {
        public IEnumerable<Warehouse> GetAll();
        public Warehouse GetById(int id);
        public void Create(WarehouseCreateModel request);
        public void Update(int id, WarehouseUpdateModel request);
        public void Delete(int id);
        public int GetNumberOfWarehouse();
    }
    public class WarehouseService : IWarehouseService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public WarehouseService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Warehouse> GetAll()
        {
            return _context.Warehouses.Include(a => a.Store).Include(a => a.WarehouseBaselines);
        }

        public Warehouse GetById(int id)
        {
            var warehouse = GetWarehouse(id);
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
            var warehouse = _context.Warehouses.Include(a => a.Store).Include(a => a.WarehouseBaselines)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (warehouse == null) throw new KeyNotFoundException("Warehouse not found!");
            return warehouse;
        }

        public int GetNumberOfWarehouse()
        {
            var numberOfWarehouse = _context.Warehouses.Count();

            return numberOfWarehouse;
        }
    }
}
