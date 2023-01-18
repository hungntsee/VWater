using BCrypt.Net;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Entity;
using Service.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Data.Entity;
using MapsterMapper;
using Repository.Model.Goods;

namespace Service.Good
{
    public interface IGoodsService
    {
        public IEnumerable<Goods> GetAll();
        public GoodsResponse GetById(int id);
        public void Create(GoodsRequest request);
        public void Update(int id, GoodsUpdateRequest request);
        public void Delete(int id);
    }
    public class GoodsService : IGoodsService
    {
        private DBContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public GoodsService(DBContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<Goods> GetAll()
        {
            return _context.Goods;
        }

        public GoodsResponse GetById(int id)
        {
            var goodsResponse = _mapper.Map<GoodsResponse>(GetGoods(id));
            return goodsResponse;
        }

        public void Create(GoodsRequest request)
        {
            if (_context.Goods.AnyAsync(g => g.GoodsName == request.GoodsName).Result)
                throw new AppException("Goods Name: '" + request.GoodsName + "' already exists");
            var goods = _mapper.Map<Goods>(request);

            _context.Goods.AddAsync(goods);
            _context.SaveChangesAsync();
        }

        public void Update(int id, GoodsUpdateRequest request)
        {
            var goods = GetGoods(id);

            if (_context.Goods.Any(g => g.GoodsName == request.GoodsName))
                throw new AppException("Goods Name: '" + request.GoodsName + "' already exists");

            goods.Adapt(request);
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
            var goods = _context.Goods.Find(id);
            if (goods == null) throw new KeyNotFoundException("Goods not found");
            return goods;
        }

    }
}
