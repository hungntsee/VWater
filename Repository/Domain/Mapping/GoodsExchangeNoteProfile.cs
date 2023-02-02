using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class GoodsExchangeNoteProfile
        : AutoMapper.Profile
    {
        public GoodsExchangeNoteProfile()
        {
            CreateMap<VWater.Data.Entities.GoodsExchangeNote, VWater.Domain.Models.GoodsExchangeNoteReadModel>();

            CreateMap<VWater.Domain.Models.GoodsExchangeNoteCreateModel, VWater.Data.Entities.GoodsExchangeNote>();

            CreateMap<VWater.Data.Entities.GoodsExchangeNote, VWater.Domain.Models.GoodsExchangeNoteUpdateModel>();

            CreateMap<VWater.Domain.Models.GoodsExchangeNoteUpdateModel, VWater.Data.Entities.GoodsExchangeNote>();

            CreateMap<VWater.Domain.Models.GoodsExchangeNoteReadModel, VWater.Domain.Models.GoodsExchangeNoteUpdateModel>();

        }

    }
}
