using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Buildings
{
    public interface IBuildingService
    {
        public IEnumerable<Building> GetAll();
        public BuildingReadModel GetById(int id);
        public void Create(BuildingCreateModel request);
        public void Update(int id, BuildingUpdateModel request);
        public void Delete(int id);
    }
    public class BuiildingService : IBuildingService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public BuiildingService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<Building> GetAll()
        {
            return _context.Buildings;
        }

        public BuildingReadModel GetById(int id)
        {
            var building = _mapper.Map<BuildingReadModel>(GetBuilding(id));
            return building;
        }

        public void Create(BuildingCreateModel request)
        {
            if (_context.Buildings.Any(b => b.BuildingName == request.BuildingName))
                throw new AppException("Building: '" + request.BuildingName + "' already exists");
            var building = _mapper.Map<Building>(request);

            _context.Buildings.AddAsync(building);
            _context.SaveChangesAsync();
        }

        public void Update(int id, BuildingUpdateModel request)
        {
            var building = GetBuilding(id);

            if (_context.Buildings.Any(b => b.BuildingName == request.BuildingName))
                throw new AppException("Building: '" + request.BuildingName + "' already exists");
            _mapper.Map(request, building);
            _context.Buildings.Update(building);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var building = GetBuilding(id);
            _context.Buildings.Remove(building);
            _context.SaveChangesAsync();
        }

        private Building GetBuilding(int id)
        {
            var building = _context.Buildings.Find(id);
            if (building == null) throw new KeyNotFoundException("Building not found!");
            return building;
        }

    }
}
