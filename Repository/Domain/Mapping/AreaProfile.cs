namespace VWater.Domain.Mapping
{
    public partial class AreaProfile
        : AutoMapper.Profile
    {
        public AreaProfile()
        {
            CreateMap<VWater.Data.Entities.Area, VWater.Domain.Models.AreaReadModel>();

            CreateMap<VWater.Domain.Models.AreaCreateModel, VWater.Data.Entities.Area>();

            CreateMap<VWater.Data.Entities.Area, VWater.Domain.Models.AreaUpdateModel>();

            CreateMap<VWater.Domain.Models.AreaUpdateModel, VWater.Data.Entities.Area>();

            CreateMap<VWater.Domain.Models.AreaReadModel, VWater.Domain.Models.AreaUpdateModel>();

        }

    }
}
