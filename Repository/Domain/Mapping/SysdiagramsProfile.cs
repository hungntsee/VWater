using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class SysdiagramsProfile
        : AutoMapper.Profile
    {
        public SysdiagramsProfile()
        {
            CreateMap<VWater.Data.Entities.Sysdiagrams, VWater.Domain.Models.SysdiagramsReadModel>();

            CreateMap<VWater.Domain.Models.SysdiagramsCreateModel, VWater.Data.Entities.Sysdiagrams>();

            CreateMap<VWater.Data.Entities.Sysdiagrams, VWater.Domain.Models.SysdiagramsUpdateModel>();

            CreateMap<VWater.Domain.Models.SysdiagramsUpdateModel, VWater.Data.Entities.Sysdiagrams>();

            CreateMap<VWater.Domain.Models.SysdiagramsReadModel, VWater.Domain.Models.SysdiagramsUpdateModel>();

        }

    }
}
