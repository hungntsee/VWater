using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.GoodsInQuotations
{
    public interface IGoodsInQuotationService
    {
        public IEnumerable<GoodsInQuotation> GetAll();
        public GoodsInQuotation GetById(int id);
        public void Create(GoodsInQuotationCreateModel request);
        public void Update(int id, GoodsInQuotationUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsInQuotationService : IGoodsInQuotationService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public GoodsInQuotationService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<GoodsInQuotation> GetAll()
        {
            return _context.GoodsInQuotations.Include(a => a.Goods).Include(a => a.Quotation);
        }

        public GoodsInQuotation GetById(int id)
        {
            var goodsInQuotation = GetGoodsInQuotation(id);
            return goodsInQuotation;
        }

        public void Create(GoodsInQuotationCreateModel request)
        {
            var goodsInQuotation = _mapper.Map<GoodsInQuotation>(request);

            _context.GoodsInQuotations.AddAsync(goodsInQuotation);
            _context.SaveChangesAsync();
        }

        public void Update(int id, GoodsInQuotationUpdateModel request)
        {
            var goodsInQuotation = GetGoodsInQuotation(id);

            _mapper.Map(request, goodsInQuotation);
            _context.GoodsInQuotations.Update(goodsInQuotation);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var goodsInQuotation = GetGoodsInQuotation(id);
            _context.GoodsInQuotations.Remove(goodsInQuotation);
            _context.SaveChangesAsync();
        }

        private GoodsInQuotation GetGoodsInQuotation(int id)
        {
            var goodsInQuotation = _context.GoodsInQuotations.Include(a => a.Goods).Include(a => a.Quotation)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (goodsInQuotation == null) throw new KeyNotFoundException("Goods In Baseline not found!");
            return goodsInQuotation;
        }

    }
}
