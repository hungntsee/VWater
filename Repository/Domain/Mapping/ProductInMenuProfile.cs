namespace VWater.Domain.Mapping
{
    public partial class ProductInMenuProfile
        : AutoMapper.Profile
    {
        public ProductInMenuProfile()
        {
            CreateMap<VWater.Data.Entities.ProductInMenu, VWater.Domain.Models.ProductInMenuReadModel>();

            CreateMap<VWater.Domain.Models.ProductInMenuCreateModel, VWater.Data.Entities.ProductInMenu>();

            CreateMap<VWater.Data.Entities.ProductInMenu, VWater.Domain.Models.ProductInMenuUpdateModel>();

            CreateMap<VWater.Domain.Models.ProductInMenuUpdateModel, VWater.Data.Entities.ProductInMenu>();

            CreateMap<VWater.Domain.Models.ProductInMenuReadModel, VWater.Domain.Models.ProductInMenuUpdateModel>();

        }

    }
}
