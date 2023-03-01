namespace VWater.Domain.Mapping
{
    public partial class GoodsInQuotationProfile
        : AutoMapper.Profile
    {
        public GoodsInQuotationProfile()
        {
            CreateMap<VWater.Data.Entities.GoodsInQuotation, VWater.Domain.Models.GoodsInQuotationReadModel>();

            CreateMap<VWater.Domain.Models.GoodsInQuotationCreateModel, VWater.Data.Entities.GoodsInQuotation>();

            CreateMap<VWater.Data.Entities.GoodsInQuotation, VWater.Domain.Models.GoodsInQuotationUpdateModel>();

            CreateMap<VWater.Domain.Models.GoodsInQuotationUpdateModel, VWater.Data.Entities.GoodsInQuotation>();

            CreateMap<VWater.Domain.Models.GoodsInQuotationReadModel, VWater.Domain.Models.GoodsInQuotationUpdateModel>();

        }

    }
}
