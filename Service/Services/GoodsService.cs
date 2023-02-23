using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Good
{
    public interface IGoodsService
    {
        public IEnumerable<Goods> GetAll();
        public Goods GetById(int id);
        public void Create(GoodsCreateModel request);
        public void Update(int id, GoodsUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsService : IGoodsService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public GoodsService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Goods> GetAll()
        {
            return _context.Goods.Include(a => a.Brand).Include(a => a.GoodsCompositions);
        }

        public Goods GetById(int id)
        {
            var goods = GetGoods(id);
            return goods;
        }

        public void Create(GoodsCreateModel request)
        {
            if (_context.Goods.AnyAsync(g => g.GoodsName == request.GoodsName).Result)
                throw new AppException("Goods: '" + request.GoodsName + "' already exists");
            var goods = _mapper.Map<Goods>(request);

            _context.Goods.AddAsync(goods);
            _context.SaveChangesAsync();
        }

        public void Update(int id, GoodsUpdateModel request)
        {
            var goods = GetGoods(id);

            if (_context.Goods.Any(g => g.GoodsName == request.GoodsName))
                throw new AppException("Goods: '" + request.GoodsName + "' already exists");
            _mapper.Map(request, goods);
            _context.Goods.Update(goods);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var goods = GetGoods(id);
            _context.Goods.Remove(goods);
            _context.SaveChangesAsync();
        }

        private Goods GetGoods(int id)
        {
            var goods = _context.Goods.Include(a => a.Brand).Include(a => a.GoodsCompositions)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (goods == null) throw new KeyNotFoundException("Goods not found!");
            return goods;
        }

    }
}
