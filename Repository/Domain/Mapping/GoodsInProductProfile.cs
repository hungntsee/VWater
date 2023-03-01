namespace VWater.Domain.Mapping
{
    public partial class GoodsInProductProfile
        : AutoMapper.Profile
    {
        public GoodsInProductProfile()
        {
            CreateMap<VWater.Data.Entities.GoodsInProduct, VWater.Domain.Models.GoodsInProductReadModel>();

            CreateMap<VWater.Domain.Models.GoodsInProductCreateModel, VWater.Data.Entities.GoodsInProduct>();

            CreateMap<VWater.Data.Entities.GoodsInProduct, VWater.Domain.Models.GoodsInProductUpdateModel>();

            CreateMap<VWater.Domain.Models.GoodsInProductUpdateModel, VWater.Data.Entities.GoodsInProduct>();

            CreateMap<VWater.Domain.Models.GoodsInProductReadModel, VWater.Domain.Models.GoodsInProductUpdateModel>();

        }

    }
}
