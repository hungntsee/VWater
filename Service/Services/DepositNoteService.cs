using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Service.Helpers;
using System.Threading.Channels;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.DepositNotes
{
    public interface IDepositNoteService
    {
        public IEnumerable<DepositNote> GetAll();
        public DepositNote GetById(int id);
        public void Create(DepositNoteCreateModel request);
        public void Update(int id, DepositNoteUpdateModel request);
        public void Delete(int id);
    }
    public class DepositNoteService : IDepositNoteService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public DepositNoteService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<DepositNote> GetAll()
        {
            return _context.DepositNotes.Include(a => a.Order);
        }

        public DepositNote GetById(int id)
        {
            var depositNote = GetDepositNote(id);
            return depositNote;
        }

        public void Create(DepositNoteCreateModel request)
        {
            var depositNote = _mapper.Map<DepositNote>(request);

            _context.DepositNotes.AddAsync(depositNote);
            _context.SaveChangesAsync();
        }

        public void Update(int id, DepositNoteUpdateModel request)
        {
            var depositNote = GetDepositNote(id);

            _mapper.Map(request, depositNote);
            _context.DepositNotes.Update(depositNote);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var depositNote = GetDepositNote(id);
            _context.DepositNotes.Remove(depositNote);
            _context.SaveChangesAsync();
        }

        private DepositNote GetDepositNote(int id)
        {
            var depositNote = _context.DepositNotes
                .Include(a => a.Order)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (depositNote == null) throw new KeyNotFoundException("DepositNote not found!");
            return depositNote;
        }

    }
}
