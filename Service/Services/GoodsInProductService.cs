using AutoMapper;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data.Entities;
using VWater.Data;
using VWater.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
        private readonly AppSetting _appSetting;

        public GoodsInProductService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
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

