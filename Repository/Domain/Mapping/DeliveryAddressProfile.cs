using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class DeliveryAddressProfile
        : AutoMapper.Profile
    {
        public DeliveryAddressProfile()
        {
            CreateMap<VWater.Data.Entities.DeliveryAddress, VWater.Domain.Models.DeliveryAddressReadModel>();

            CreateMap<VWater.Domain.Models.DeliveryAddressCreateModel, VWater.Data.Entities.DeliveryAddress>();

            CreateMap<VWater.Data.Entities.DeliveryAddress, VWater.Domain.Models.DeliveryAddressUpdateModel>();

            CreateMap<VWater.Domain.Models.DeliveryAddressUpdateModel, VWater.Data.Entities.DeliveryAddress>();

            CreateMap<VWater.Domain.Models.DeliveryAddressReadModel, VWater.Domain.Models.DeliveryAddressUpdateModel>();

        }

    }
}
