namespace VWater.Domain.Mapping
{
    public partial class PurchaseOrderProfile
        : AutoMapper.Profile
    {
        public PurchaseOrderProfile()
        {
            CreateMap<VWater.Data.Entities.PurchaseOrder, VWater.Domain.Models.PurchaseOrderReadModel>();

            CreateMap<VWater.Domain.Models.PurchaseOrderCreateModel, VWater.Data.Entities.PurchaseOrder>();

            CreateMap<VWater.Data.Entities.PurchaseOrder, VWater.Domain.Models.PurchaseOrderUpdateModel>();

            CreateMap<VWater.Domain.Models.PurchaseOrderUpdateModel, VWater.Data.Entities.PurchaseOrder>();

            CreateMap<VWater.Domain.Models.PurchaseOrderReadModel, VWater.Domain.Models.PurchaseOrderUpdateModel>();

        }

    }
}
