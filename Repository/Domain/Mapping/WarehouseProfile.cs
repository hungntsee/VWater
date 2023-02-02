using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class WarehouseProfile
        : AutoMapper.Profile
    {
        public WarehouseProfile()
        {
            CreateMap<VWater.Data.Entities.Warehouse, VWater.Domain.Models.WarehouseReadModel>();

            CreateMap<VWater.Domain.Models.WarehouseCreateModel, VWater.Data.Entities.Warehouse>();

            CreateMap<VWater.Data.Entities.Warehouse, VWater.Domain.Models.WarehouseUpdateModel>();

            CreateMap<VWater.Domain.Models.WarehouseUpdateModel, VWater.Data.Entities.Warehouse>();

            CreateMap<VWater.Domain.Models.WarehouseReadModel, VWater.Domain.Models.WarehouseUpdateModel>();

        }

    }
}
