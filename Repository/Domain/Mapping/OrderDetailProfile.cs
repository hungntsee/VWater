namespace VWater.Domain.Mapping
{
    public partial class OrderDetailProfile
        : AutoMapper.Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<VWater.Data.Entities.OrderDetail, VWater.Domain.Models.OrderDetailReadModel>();

            CreateMap<VWater.Domain.Models.OrderDetailCreateModel, VWater.Data.Entities.OrderDetail>();

            CreateMap<VWater.Data.Entities.OrderDetail, VWater.Domain.Models.OrderDetailUpdateModel>();

            CreateMap<VWater.Domain.Models.OrderDetailUpdateModel, VWater.Data.Entities.OrderDetail>();

            CreateMap<VWater.Domain.Models.OrderDetailReadModel, VWater.Domain.Models.OrderDetailUpdateModel>();

        }

    }
}
