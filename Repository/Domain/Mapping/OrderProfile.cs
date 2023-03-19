namespace VWater.Domain.Mapping
{
    public partial class OrderProfile
        : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<VWater.Data.Entities.Order, VWater.Domain.Models.OrderReadModel>();

            CreateMap<VWater.Domain.Models.OrderCreateModel, VWater.Data.Entities.Order>();

            CreateMap<VWater.Data.Entities.Order, VWater.Domain.Models.OrderCreateModel>();

            CreateMap<VWater.Data.Entities.Order, VWater.Domain.Models.OrderUpdateModel>();

            CreateMap<VWater.Domain.Models.OrderUpdateModel, VWater.Data.Entities.Order>();

            CreateMap<VWater.Domain.Models.OrderReadModel, VWater.Domain.Models.OrderUpdateModel>();

        }

    }
}
