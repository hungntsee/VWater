using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class RolesProfile
        : AutoMapper.Profile
    {
        public RolesProfile()
        {
            CreateMap<VWater.Data.Entities.Roles, VWater.Domain.Models.RolesReadModel>();

            CreateMap<VWater.Domain.Models.RolesCreateModel, VWater.Data.Entities.Roles>();

            CreateMap<VWater.Data.Entities.Roles, VWater.Domain.Models.RolesUpdateModel>();

            CreateMap<VWater.Domain.Models.RolesUpdateModel, VWater.Data.Entities.Roles>();

            CreateMap<VWater.Domain.Models.RolesReadModel, VWater.Domain.Models.RolesUpdateModel>();

        }

    }
}
