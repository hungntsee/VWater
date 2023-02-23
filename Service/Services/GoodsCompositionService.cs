﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.GoodsCompositions
{
    public interface IGoodsCompositionService
    {
        public IEnumerable<GoodsComposition> GetAll();
        public GoodsComposition GetById(int id);
        public void Create(GoodsCompositionCreateModel request);
        public void Update(int id, GoodsCompositionUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsCompositionService : IGoodsCompositionService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public GoodsCompositionService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<GoodsComposition> GetAll()
        {
            return _context.GoodsCompositions.Include(a => a.Goods);
        }

        public GoodsComposition GetById(int id)
        {
            var goodsComposition = GetGoodsComposition(id);
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
            var goodsComposition = _context.GoodsCompositions.Include(a => a.Goods).AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (goodsComposition == null) throw new KeyNotFoundException("Goods composition not found!");
            return goodsComposition;
        }
    }
}
