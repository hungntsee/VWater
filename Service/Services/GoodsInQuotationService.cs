﻿using AutoMapper;
using Microsoft.Extensions.Options;
using Service.Helpers;
using VWater.Data.Entities;
using VWater.Data;
using VWater.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Service.GoodsInQuotations
{
    public interface IGoodsInQuotationService
    {
        public IEnumerable<GoodsInQuotation> GetAll();
        public GoodsInQuotationReadModel GetById(int id);
        public void Create(GoodsInQuotationCreateModel request);
        public void Update(int id, GoodsInQuotationUpdateModel request);
        public void Delete(int id);
    }
    public class GoodsInQuotationService : IGoodsInQuotationService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public GoodsInQuotationService(VWaterContext context, IOptions<AppSetting> appSetting, IMapper mapper)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }
        public IEnumerable<GoodsInQuotation> GetAll()
        {
            return _context.GoodsInQuotations;
        }

        public GoodsInQuotationReadModel GetById(int id)
        {
            var goodsInQuotation = _mapper.Map<GoodsInQuotationReadModel>(GetGoodsInQuotation(id));
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
            var goodsInQuotation = _context.GoodsInQuotations.Find(id);
            if (goodsInQuotation == null) throw new KeyNotFoundException("Goods In Baseline not found!");
            return goodsInQuotation;
        }

    }
}
