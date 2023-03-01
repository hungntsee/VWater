namespace VWater.Domain.Mapping
{
    public partial class StoreProfile
        : AutoMapper.Profile
    {
        public StoreProfile()
        {
            CreateMap<VWater.Data.Entities.Store, VWater.Domain.Models.StoreReadModel>();

            CreateMap<VWater.Domain.Models.StoreCreateModel, VWater.Data.Entities.Store>();

            CreateMap<VWater.Data.Entities.Store, VWater.Domain.Models.StoreUpdateModel>();

            CreateMap<VWater.Domain.Models.StoreUpdateModel, VWater.Data.Entities.Store>();

            CreateMap<VWater.Domain.Models.StoreReadModel, VWater.Domain.Models.StoreUpdateModel>();

        }

    }
}
