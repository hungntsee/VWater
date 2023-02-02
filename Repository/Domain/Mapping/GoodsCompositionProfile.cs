using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class GoodsCompositionProfile
        : AutoMapper.Profile
    {
        public GoodsCompositionProfile()
        {
            CreateMap<VWater.Data.Entities.GoodsComposition, VWater.Domain.Models.GoodsCompositionReadModel>();

            CreateMap<VWater.Domain.Models.GoodsCompositionCreateModel, VWater.Data.Entities.GoodsComposition>();

            CreateMap<VWater.Data.Entities.GoodsComposition, VWater.Domain.Models.GoodsCompositionUpdateModel>();

            CreateMap<VWater.Domain.Models.GoodsCompositionUpdateModel, VWater.Data.Entities.GoodsComposition>();

            CreateMap<VWater.Domain.Models.GoodsCompositionReadModel, VWater.Domain.Models.GoodsCompositionUpdateModel>();

        }

    }
}
