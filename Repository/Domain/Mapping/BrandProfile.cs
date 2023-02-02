using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class BrandProfile
        : AutoMapper.Profile
    {
        public BrandProfile()
        {
            CreateMap<VWater.Data.Entities.Brand, VWater.Domain.Models.BrandReadModel>();

            CreateMap<VWater.Domain.Models.BrandCreateModel, VWater.Data.Entities.Brand>();

            CreateMap<VWater.Data.Entities.Brand, VWater.Domain.Models.BrandUpdateModel>();

            CreateMap<VWater.Domain.Models.BrandUpdateModel, VWater.Data.Entities.Brand>();

            CreateMap<VWater.Domain.Models.BrandReadModel, VWater.Domain.Models.BrandUpdateModel>();

        }

    }
}
