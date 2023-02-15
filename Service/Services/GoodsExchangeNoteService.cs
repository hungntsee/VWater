using Microsoft.EntityFrameworkCore;
using VWater.Data.Entities;
using VWater.Domain.Models;
using Microsoft.Extensions.Options;
using AutoMapper;
using VWater.Data;
using Service.Helpers;

namespace Service.GoodExchangeNote
{
    public interface IGoodsExchangeNoteService
    {
        public IEnumerable<GoodsExchangeNote> GetAll();
        public GoodsExchangeNoteReadModel GetById(int id);
        public void Create(GoodsExchangeNoteCreateModel request);
        public void Update(int id, GoodsExchangeNoteUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsExchangeNoteService : IGoodsExchangeNoteService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public GoodsExchangeNoteService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<GoodsExchangeNote> GetAll()
        {
            return _context.GoodsExchangeNotes;
        }

        public GoodsExchangeNoteReadModel GetById(int id)
        {
            var goodsExchangeNote = _mapper.Map<GoodsExchangeNoteReadModel>(GetGoodsExchangeNote(id));
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
            var goodsExchangeNote = _context.GoodsExchangeNotes.Find(id);
            if (goodsExchangeNote == null) throw new KeyNotFoundException("Goods Exchange Note not found!");
            return goodsExchangeNote;
        }
    }
    }
