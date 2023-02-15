using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Areas
{
    public interface IAreaService
    {
        public IEnumerable<Area> GetAll();
        public AreaReadModel GetById(int id);
        public void Create(AreaCreateModel request);
        public void Update(int id, AreaUpdateModel request);
        public void Delete(int id);
    }
    public class AreaService : IAreaService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public AreaService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<Area> GetAll()
        {
            return _context.Areas;
        }

        public AreaReadModel GetById(int id)
        {
            var area = _mapper.Map<AreaReadModel>(GetArea(id));
            return area;
        }

        public void Create(AreaCreateModel request)
        {
            if (_context.Areas.Any(a => a.AreaName == request.AreaName))
                throw new AppException("Area: '" + request.AreaName + "' already exists");
            var area = _mapper.Map<Area>(request);

            _context.Areas.AddAsync(area);
            _context.SaveChangesAsync();
        }

        public void Update(int id, AreaUpdateModel request)
        {
            var area = GetArea(id);

            if (_context.Areas.Any(a => a.AreaName == request.AreaName))
                throw new AppException("Area: '" + request.AreaName + "' already exists");
            _mapper.Map(request, area);
            _context.Areas.Update(area);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var area = GetArea(id);
            _context.Areas.Remove(area);
            _context.SaveChangesAsync();
        }

        private Area GetArea(int id)
        {
            var area = _context.Areas.Find(id);
            if (area == null) throw new KeyNotFoundException("Area not found!");
            return area;
        }

    }
}
