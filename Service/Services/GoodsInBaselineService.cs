using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.GoodsInBaselines
{
    public interface IGoodsInBaselineService
    {
        public IEnumerable<GoodsInBaseline> GetAll();
        public GoodsInBaseline GetById(int id);
        public void Create(GoodsInBaselineCreateModel request);
        public void Update(int id, GoodsInBaselineUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsInBaselineService : IGoodsInBaselineService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;


        public GoodsInBaselineService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<GoodsInBaseline> GetAll()
        {
            return _context.GoodsInBaselines.Include(a => a.Goods).Include(a => a.WarehouseBaseline);
        }

        public GoodsInBaseline GetById(int id)
        {
            var goodsInBaseline = GetGoodsInBaseline(id);
            return goodsInBaseline;
        }

        public void Create(GoodsInBaselineCreateModel request)
        {
            var goodsInBaseline = _mapper.Map<GoodsInBaseline>(request);

            _context.GoodsInBaselines.AddAsync(goodsInBaseline);
            _context.SaveChangesAsync();
        }

        public void Update(int id, GoodsInBaselineUpdateModel request)
        {
            var goodsInBaseline = GetGoodsInBaseline(id);

            _mapper.Map(request, goodsInBaseline);
            _context.GoodsInBaselines.Update(goodsInBaseline);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var goodsInBaseline = GetGoodsInBaseline(id);
            _context.GoodsInBaselines.Remove(goodsInBaseline);
            _context.SaveChangesAsync();
        }

        private GoodsInBaseline GetGoodsInBaseline(int id)
        {
            var goodsInBaseline = _context.GoodsInBaselines.Include(a => a.Goods).Include(a => a.WarehouseBaseline)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (goodsInBaseline == null) throw new KeyNotFoundException("Goods In Baseline not found!");
            return goodsInBaseline;
        }

    }
}
