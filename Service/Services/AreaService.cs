using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Service.Helpers;
using System.Threading.Channels;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Areas
{
    public interface IAreaService
    {
        public IEnumerable<Area> GetAll();
        public Area GetById(int id);
        public void Create(AreaCreateModel request);
        public void Update(int id, AreaUpdateModel request);
        public void Delete(int id);
        public int GetNumberOfArea();
    }
    public class AreaService : IAreaService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public AreaService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Area> GetAll()
        {
            return _context.Areas.Include(a => a.Distributors).Include(a => a.Apartments).Include(a => a.Stores).Include(a => a.Menus);
        }

        public Area GetById(int id)
        {
            var area = GetArea(id);
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
            var area = _context.Areas
                .Include(a => a.Distributors)
                .Include(a => a.Apartments)
                .Include(a => a.Stores)
                .Include(a => a.Menus)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (area == null) throw new KeyNotFoundException("Area not found!");
            return area;
        }

        public int GetNumberOfArea()
        {
            return _context.Areas.Count();
        }

    }
}
