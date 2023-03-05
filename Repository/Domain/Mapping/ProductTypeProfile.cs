namespace VWater.Domain.Mapping
{
    public partial class ProductTypeProfile
        : AutoMapper.Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<VWater.Data.Entities.ProductType, VWater.Domain.Models.ProductTypeReadModel>();

            CreateMap<VWater.Domain.Models.ProductTypeCreateModel, VWater.Data.Entities.ProductType>();

            CreateMap<VWater.Data.Entities.ProductType, VWater.Domain.Models.ProductTypeUpdateModel>();

            CreateMap<VWater.Domain.Models.ProductTypeUpdateModel, VWater.Data.Entities.ProductType>();

            CreateMap<VWater.Domain.Models.ProductTypeReadModel, VWater.Domain.Models.ProductTypeUpdateModel>();

        }

    }
}
