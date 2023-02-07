using AutoMapper;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data.Entities;
using VWater.Data;
using VWater.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Service.GoodsCompositions
{
    public interface IGoodsCompositionService
    {
        public IEnumerable<GoodsComposition> GetAll();
        public GoodsCompositionReadModel GetById(int id);
        public void Create(GoodsCompositionCreateModel request);
        public void Update(int id, GoodsCompositionUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsCompositionService : IGoodsCompositionService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public GoodsCompositionService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<GoodsComposition> GetAll()
        {
            return _context.GoodsCompositions;
        }

        public GoodsCompositionReadModel GetById(int id)
        {
            var goodsComposition = _mapper.Map<GoodsCompositionReadModel>(GetGoodsComposition(id));
            return goodsComposition;
        }

        public void Create(GoodsCompositionCreateModel request)
        {
            if (_context.GoodsCompositions.AnyAsync(n => n.Name == request.Name).Result)
                throw new AppException("Goods Composition: '" + request.Name + "'is already exists");
            var goodsComposition = _mapper.Map<GoodsComposition>(request);

            _context.GoodsCompositions.AddAsync(goodsComposition);
            _context.SaveChangesAsync();
        }

        public void Update(int id, GoodsCompositionUpdateModel request)
        {
            var goodsComposition = GetGoodsComposition(id);

            if (_context.GoodsCompositions.Any(n => n.Name == request.Name))
                throw new AppException("Goods Composition: '" + request.Name + "'is already exists");

            _mapper.Map(request, goodsComposition);
            _context.GoodsCompositions.Update(goodsComposition);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var goodsComposition = GetGoodsComposition(id);
            _context.GoodsCompositions.Remove(goodsComposition);
            _context.SaveChangesAsync();
        }

        private GoodsComposition GetGoodsComposition(int id)
        {
            var goodsComposition = _context.GoodsCompositions.Find(id);
            if (goodsComposition == null) throw new KeyNotFoundException("Goods composition not found!");
            return goodsComposition;
        }
    }
}
