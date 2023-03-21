using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Buildings
{
    public interface IBuildingService
    {
        public IEnumerable<Building> GetAll();
        public Building GetById(int id);
        public void Create(BuildingCreateModel request);
        public void Update(int id, BuildingUpdateModel request);
        public void Delete(int id);
    }
    public class BuiildingService : IBuildingService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public BuiildingService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Building> GetAll()
        {
            var buildings = _context.Buildings.Include(a => a.Apartment).Include(a => a.DeliveryAddresses);
            foreach(var building in buildings)
            {
                building.Apartment.Buildings = null;
            }
            return buildings;
        }

        public Building GetById(int id)
        {
            var building = GetBuilding(id);
            return building;
        }

        public void Create(BuildingCreateModel request)
        {
            var building = _mapper.Map<Building>(request);

            _context.Buildings.AddAsync(building);
            _context.SaveChangesAsync();
        }

        public void Update(int id, BuildingUpdateModel request)
        {
            var building = GetBuilding(id);
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
            var building = _context.Buildings.Include(a => a.Apartment).Include(a => a.DeliveryAddresses)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (building == null) throw new KeyNotFoundException("Building not found!");
            return building;
        }

    }
}
