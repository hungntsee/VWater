using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class GoodsInBaselineProfile
        : AutoMapper.Profile
    {
        public GoodsInBaselineProfile()
        {
            CreateMap<VWater.Data.Entities.GoodsInBaseline, VWater.Domain.Models.GoodsInBaselineReadModel>();

            CreateMap<VWater.Domain.Models.GoodsInBaselineCreateModel, VWater.Data.Entities.GoodsInBaseline>();

            CreateMap<VWater.Data.Entities.GoodsInBaseline, VWater.Domain.Models.GoodsInBaselineUpdateModel>();

            CreateMap<VWater.Domain.Models.GoodsInBaselineUpdateModel, VWater.Data.Entities.GoodsInBaseline>();

            CreateMap<VWater.Domain.Models.GoodsInBaselineReadModel, VWater.Domain.Models.GoodsInBaselineUpdateModel>();

        }

    }
}
