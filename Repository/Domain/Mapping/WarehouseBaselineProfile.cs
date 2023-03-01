namespace VWater.Domain.Mapping
{
    public partial class WarehouseBaselineProfile
        : AutoMapper.Profile
    {
        public WarehouseBaselineProfile()
        {
            CreateMap<VWater.Data.Entities.WarehouseBaseline, VWater.Domain.Models.WarehouseBaselineReadModel>();

            CreateMap<VWater.Domain.Models.WarehouseBaselineCreateModel, VWater.Data.Entities.WarehouseBaseline>();

            CreateMap<VWater.Data.Entities.WarehouseBaseline, VWater.Domain.Models.WarehouseBaselineUpdateModel>();

            CreateMap<VWater.Domain.Models.WarehouseBaselineUpdateModel, VWater.Data.Entities.WarehouseBaseline>();

            CreateMap<VWater.Domain.Models.WarehouseBaselineReadModel, VWater.Domain.Models.WarehouseBaselineUpdateModel>();

        }

    }
}
