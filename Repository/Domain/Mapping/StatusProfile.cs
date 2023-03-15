using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class StatusProfile
        : AutoMapper.Profile
    {
        public StatusProfile()
        {
            CreateMap<VWater.Data.Entities.Status, VWater.Domain.Models.StatusReadModel>();

            CreateMap<VWater.Domain.Models.StatusCreateModel, VWater.Data.Entities.Status>();

            CreateMap<VWater.Data.Entities.Status, VWater.Domain.Models.StatusUpdateModel>();

            CreateMap<VWater.Domain.Models.StatusUpdateModel, VWater.Data.Entities.Status>();

            CreateMap<VWater.Domain.Models.StatusReadModel, VWater.Domain.Models.StatusUpdateModel>();

        }

    }
}
