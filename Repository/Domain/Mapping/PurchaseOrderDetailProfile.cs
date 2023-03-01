namespace VWater.Domain.Mapping
{
    public partial class PurchaseOrderDetailProfile
        : AutoMapper.Profile
    {
        public PurchaseOrderDetailProfile()
        {
            CreateMap<VWater.Data.Entities.PurchaseOrderDetail, VWater.Domain.Models.PurchaseOrderDetailReadModel>();

            CreateMap<VWater.Domain.Models.PurchaseOrderDetailCreateModel, VWater.Data.Entities.PurchaseOrderDetail>();

            CreateMap<VWater.Data.Entities.PurchaseOrderDetail, VWater.Domain.Models.PurchaseOrderDetailUpdateModel>();

            CreateMap<VWater.Domain.Models.PurchaseOrderDetailUpdateModel, VWater.Data.Entities.PurchaseOrderDetail>();

            CreateMap<VWater.Domain.Models.PurchaseOrderDetailReadModel, VWater.Domain.Models.PurchaseOrderDetailUpdateModel>();

        }

    }
}
