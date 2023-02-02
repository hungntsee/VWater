using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class ApartmentProfile
        : AutoMapper.Profile
    {
        public ApartmentProfile()
        {
            CreateMap<VWater.Data.Entities.Apartment, VWater.Domain.Models.ApartmentReadModel>();

            CreateMap<VWater.Domain.Models.ApartmentCreateModel, VWater.Data.Entities.Apartment>();

            CreateMap<VWater.Data.Entities.Apartment, VWater.Domain.Models.ApartmentUpdateModel>();

            CreateMap<VWater.Domain.Models.ApartmentUpdateModel, VWater.Data.Entities.Apartment>();

            CreateMap<VWater.Domain.Models.ApartmentReadModel, VWater.Domain.Models.ApartmentUpdateModel>();

        }

    }
}
