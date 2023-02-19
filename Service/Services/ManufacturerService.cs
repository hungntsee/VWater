using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Manufacturers
{
    public interface IManufacturerService
    {
        public IEnumerable<Manufacturer> GetAll();
        public ManufacturerReadModel GetById(int id);
        public void Create(ManufacturerCreateModel request);
        public void Update(int id, ManufacturerUpdateModel request);
        public void Delete(int id);
    }
    public class ManufacturerService : IManufacturerService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public ManufacturerService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<Manufacturer> GetAll()
        {
            return _context.Manufacturers;
        }

        public ManufacturerReadModel GetById(int id)
        {
            var manufacturer = _mapper.Map<ManufacturerReadModel>(GetManufacturer(id));
            return manufacturer;
        }

        public void Create(ManufacturerCreateModel request)
        {
            /*
            if (_context.Manufacturers.Any(m => m.ManufacturerName == request.ManufacturerName))
                throw new AppException("Manufacturer: '" + request.ManufacturerName + "' already exists");
            */
            var manufacturer = _mapper.Map<Manufacturer>(request);

            _context.Manufacturers.AddAsync(manufacturer);
            _context.SaveChangesAsync();
        }

        public void Update(int id, ManufacturerUpdateModel request)
        {
            var manufacturer = GetManufacturer(id);

            /*
            if (_context.Manufacturers.Any(m => m.ManufacturerName == request.ManufacturerName))
                throw new AppException("Manufacturer: '" + request.ManufacturerName + "' already exists");
            */
            _mapper.Map(request, manufacturer);
            _context.Manufacturers.Update(manufacturer);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var manufacturer = GetManufacturer(id);
            _context.Manufacturers.Remove(manufacturer);
            _context.SaveChangesAsync();
        }

        private Manufacturer GetManufacturer(int id)
        {
            var manufacturer = _context.Manufacturers.Find(id);
            if (manufacturer == null) throw new KeyNotFoundException("Manufacturer not found!");
            return manufacturer;
        }

    }
}
