using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.GoodExchangeNote
{
    public interface IGoodsExchangeNoteService
    {
        public IEnumerable<GoodsExchangeNote> GetAll();
        public GoodsExchangeNote GetById(int id);
        public void Create(GoodsExchangeNoteCreateModel request);
        public void Update(int id, GoodsExchangeNoteUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsExchangeNoteService : IGoodsExchangeNoteService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public GoodsExchangeNoteService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<GoodsExchangeNote> GetAll()
        {
            return _context.GoodsExchangeNotes.Include(a => a.PurchaseOrder).Include(a => a.Goods).Include(a => a.Order);
        }

        public GoodsExchangeNote GetById(int id)
        {
            var goodsExchangeNote = GetGoodsExchangeNote(id);
            return goodsExchangeNote;
        }

        public void Create(GoodsExchangeNoteCreateModel request)
        {

            var goodsExchangeNote = _mapper.Map<GoodsExchangeNote>(request);

            _context.GoodsExchangeNotes.AddAsync(goodsExchangeNote);
            _context.SaveChangesAsync();
        }

        public void Update(int id, GoodsExchangeNoteUpdateModel request)
        {
            var goodsExchangeNote = GetGoodsExchangeNote(id);



            _mapper.Map(request, goodsExchangeNote);
            _context.GoodsExchangeNotes.Update(goodsExchangeNote);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var goodsExchangeNote = GetGoodsExchangeNote(id);
            _context.GoodsExchangeNotes.Remove(goodsExchangeNote);
            _context.SaveChangesAsync();
        }

        private GoodsExchangeNote GetGoodsExchangeNote(int id)
        {
            var goodsExchangeNote = _context.GoodsExchangeNotes.Include(a => a.PurchaseOrder).Include(a => a.Goods).Include(a => a.Order)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (goodsExchangeNote == null) throw new KeyNotFoundException("Goods Exchange Note not found!");
            return goodsExchangeNote;
        }
    }
}
