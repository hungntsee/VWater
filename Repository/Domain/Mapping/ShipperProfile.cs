namespace VWater.Domain.Mapping
{
    public partial class ShipperProfile
        : AutoMapper.Profile
    {
        public ShipperProfile()
        {
            CreateMap<VWater.Data.Entities.Shipper, VWater.Domain.Models.ShipperReadModel>();

            CreateMap<VWater.Domain.Models.ShipperCreateModel, VWater.Data.Entities.Shipper>();

            CreateMap<VWater.Data.Entities.Shipper, VWater.Domain.Models.ShipperUpdateModel>();

            CreateMap<VWater.Domain.Models.ShipperUpdateModel, VWater.Data.Entities.Shipper>();

            CreateMap<VWater.Domain.Models.ShipperReadModel, VWater.Domain.Models.ShipperUpdateModel>();


            CreateMap<VWater.Data.Entities.Shipper, VWater.Domain.Models.ShipperStatusModel>();
            CreateMap<VWater.Domain.Models.ShipperStatusModel, VWater.Data.Entities.Shipper>();
            CreateMap<VWater.Domain.Models.ShipperReadModel, VWater.Domain.Models.ShipperStatusModel>();
        }

    }
}
