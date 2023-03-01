namespace VWater.Domain.Mapping
{
    public partial class BuildingProfile
        : AutoMapper.Profile
    {
        public BuildingProfile()
        {
            CreateMap<VWater.Data.Entities.Building, VWater.Domain.Models.BuildingReadModel>();

            CreateMap<VWater.Domain.Models.BuildingCreateModel, VWater.Data.Entities.Building>();

            CreateMap<VWater.Data.Entities.Building, VWater.Domain.Models.BuildingUpdateModel>();

            CreateMap<VWater.Domain.Models.BuildingUpdateModel, VWater.Data.Entities.Building>();

            CreateMap<VWater.Domain.Models.BuildingReadModel, VWater.Domain.Models.BuildingUpdateModel>();

        }

    }
}
