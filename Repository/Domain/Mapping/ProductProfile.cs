namespace VWater.Domain.Mapping
{
    public partial class ProductProfile
        : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<VWater.Data.Entities.Product, VWater.Domain.Models.ProductReadModel>();

            CreateMap<VWater.Domain.Models.ProductCreateModel, VWater.Data.Entities.Product>();

            CreateMap<VWater.Data.Entities.Product, VWater.Domain.Models.ProductUpdateModel>();

            CreateMap<VWater.Domain.Models.ProductUpdateModel, VWater.Data.Entities.Product>();

            CreateMap<VWater.Domain.Models.ProductReadModel, VWater.Domain.Models.ProductUpdateModel>();

        }

    }
}
