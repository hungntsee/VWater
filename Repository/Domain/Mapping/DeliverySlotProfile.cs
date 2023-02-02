using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class DeliverySlotProfile
        : AutoMapper.Profile
    {
        public DeliverySlotProfile()
        {
            CreateMap<VWater.Data.Entities.DeliverySlot, VWater.Domain.Models.DeliverySlotReadModel>();

            CreateMap<VWater.Domain.Models.DeliverySlotCreateModel, VWater.Data.Entities.DeliverySlot>();

            CreateMap<VWater.Data.Entities.DeliverySlot, VWater.Domain.Models.DeliverySlotUpdateModel>();

            CreateMap<VWater.Domain.Models.DeliverySlotUpdateModel, VWater.Data.Entities.DeliverySlot>();

            CreateMap<VWater.Domain.Models.DeliverySlotReadModel, VWater.Domain.Models.DeliverySlotUpdateModel>();

        }

    }
}
