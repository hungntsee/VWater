using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IStatusService
    {
        public IEnumerable<Status> GetAll();
        public Status GetById(int id);
        public void Create(StatusCreateModel request);
        public void Update(int id, StatusUpdateModel request);
        public void Delete(int id);
    }
    public class StatusService : IStatusService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public StatusService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Status> GetAll()
        {
            return _context.Statuses;
        }

        public void Create(StatusCreateModel request)
        {
            var status = _mapper.Map<Status>(request);
            _context.Statuses.Add(status);
            _context.SaveChanges();
        }

        public void Update(int id, StatusUpdateModel request)
        {
            var status = GetStatusById(id);
            _mapper.Map(status, request);

            _context.Statuses.Update(status);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var status = GetStatusById(id);

            _context.Statuses.Remove(status); 
            _context.SaveChanges();
        }

        public Status GetById(int id) 
        {
            return GetStatusById(id);
        }

        private Status GetStatusById(int id)
        {
            return _context.Statuses.Include(a => a.Orders).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
    }
}
