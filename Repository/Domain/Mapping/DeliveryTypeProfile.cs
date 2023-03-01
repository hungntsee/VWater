namespace VWater.Domain.Mapping
{
    public partial class DeliveryTypeProfile
        : AutoMapper.Profile
    {
        public DeliveryTypeProfile()
        {
            CreateMap<VWater.Data.Entities.DeliveryType, VWater.Domain.Models.DeliveryTypeReadModel>();

            CreateMap<VWater.Domain.Models.DeliveryTypeCreateModel, VWater.Data.Entities.DeliveryType>();

            CreateMap<VWater.Data.Entities.DeliveryType, VWater.Domain.Models.DeliveryTypeUpdateModel>();

            CreateMap<VWater.Domain.Models.DeliveryTypeUpdateModel, VWater.Data.Entities.DeliveryType>();

            CreateMap<VWater.Domain.Models.DeliveryTypeReadModel, VWater.Domain.Models.DeliveryTypeUpdateModel>();

        }

    }
}
