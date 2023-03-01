namespace VWater.Domain.Mapping
{
    public partial class QuotationProfile
        : AutoMapper.Profile
    {
        public QuotationProfile()
        {
            CreateMap<VWater.Data.Entities.Quotation, VWater.Domain.Models.QuotationReadModel>();

            CreateMap<VWater.Domain.Models.QuotationCreateModel, VWater.Data.Entities.Quotation>();

            CreateMap<VWater.Data.Entities.Quotation, VWater.Domain.Models.QuotationUpdateModel>();

            CreateMap<VWater.Domain.Models.QuotationUpdateModel, VWater.Data.Entities.Quotation>();

            CreateMap<VWater.Domain.Models.QuotationReadModel, VWater.Domain.Models.QuotationUpdateModel>();

        }

    }
}
