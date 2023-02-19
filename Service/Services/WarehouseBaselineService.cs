using AutoMapper;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IWarehouseBaselineService
    {
        public IEnumerable<WarehouseBaseline> GetAll();
        public WarehouseBaselineReadModel GetById(int id);
        public void Create(WarehouseBaselineCreateModel model);
        public void Update(int id, WarehouseBaselineUpdateModel model);
        public void Delete(int id);
    }
    public class WarehouseBaselineService: IWarehouseBaselineService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        public WarehouseBaselineService (VWaterContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(WarehouseBaselineCreateModel model)
        {
            var warehouseBaseline = _mapper.Map<WarehouseBaseline>(model);
            _context.WarehouseBaselines.AddAsync(warehouseBaseline);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var warehouseBaseline = GetWarehouseBaseline(id);
            _context.WarehouseBaselines.Remove(warehouseBaseline);
            _context.SaveChangesAsync();
        }

        public IEnumerable<WarehouseBaseline> GetAll()
        {
            return _context.WarehouseBaselines;
        }

        public WarehouseBaselineReadModel GetById(int id)
        {
            var warehouseBaseline = _mapper.Map<WarehouseBaselineReadModel>(GetWarehouseBaseline(id));
            return warehouseBaseline;
        }

        public void Update(int id, WarehouseBaselineUpdateModel model)
        {
            var warehouseBaseline = GetWarehouseBaseline(id);
            _mapper.Map(warehouseBaseline, model);
            _context.WarehouseBaselines.Update(warehouseBaseline);
            _context.SaveChangesAsync();
        }

        private WarehouseBaseline GetWarehouseBaseline(int id)
        {
            var warehouseBaseline = _context.WarehouseBaselines.FirstOrDefault(p => p.Id == id);
            return warehouseBaseline;
        }
    }
}
