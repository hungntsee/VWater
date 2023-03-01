namespace VWater.Domain.Mapping
{
    public partial class MenuProfile
        : AutoMapper.Profile
    {
        public MenuProfile()
        {
            CreateMap<VWater.Data.Entities.Menu, VWater.Domain.Models.MenuReadModel>();

            CreateMap<VWater.Domain.Models.MenuCreateModel, VWater.Data.Entities.Menu>();

            CreateMap<VWater.Data.Entities.Menu, VWater.Domain.Models.MenuUpdateModel>();

            CreateMap<VWater.Domain.Models.MenuUpdateModel, VWater.Data.Entities.Menu>();

            CreateMap<VWater.Domain.Models.MenuReadModel, VWater.Domain.Models.MenuUpdateModel>();

        }

    }
}
