using AutoMapper;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.GoodsInProducts
{
    public interface IGoodsInProductService
    {
        public IEnumerable<GoodsInProduct> GetAll();
        public GoodsInProductReadModel GetById(int id);
        public void Create(GoodsInProductCreateModel request);
        public void Update(int id, GoodsInProductUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsInProductService : IGoodsInProductService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public GoodsInProductService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<GoodsInProduct> GetAll()
        {
            return _context.GoodsInProducts;
        }

        public GoodsInProductReadModel GetById(int id)
        {
            var goodsInProduct = _mapper.Map<GoodsInProductReadModel>(GetGoodsInProduct(id));
            return goodsInProduct;
        }

        public void Create(GoodsInProductCreateModel request)
        {
            var goodsInProduct = _mapper.Map<GoodsInProduct>(request);

            _context.GoodsInProducts.AddAsync(goodsInProduct);
            _context.SaveChangesAsync();
        }

        public void Update(int id, GoodsInProductUpdateModel request)
        {
            var goodsInProduct = GetGoodsInProduct(id);
            _mapper.Map(request, goodsInProduct);
            _context.GoodsInProducts.Update(goodsInProduct);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var goodsInProduct = GetGoodsInProduct(id);
            _context.GoodsInProducts.Remove(goodsInProduct);
            _context.SaveChangesAsync();
        }

        private GoodsInProduct GetGoodsInProduct(int id)
        {
            var goodsInProduct = _context.GoodsInProducts.Find(id);
            if (goodsInProduct == null) throw new KeyNotFoundException("Goods In Product not found!");
            return goodsInProduct;
        }

    }
}

