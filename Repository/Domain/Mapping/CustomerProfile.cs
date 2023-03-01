namespace VWater.Domain.Mapping
{
    public partial class CustomerProfile
        : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<VWater.Data.Entities.Customer, VWater.Domain.Models.CustomerReadModel>();

            CreateMap<VWater.Domain.Models.CustomerCreateModel, VWater.Data.Entities.Customer>();

            CreateMap<VWater.Data.Entities.Customer, VWater.Domain.Models.CustomerUpdateModel>();

            CreateMap<VWater.Domain.Models.CustomerUpdateModel, VWater.Data.Entities.Customer>();

            CreateMap<VWater.Domain.Models.CustomerReadModel, VWater.Domain.Models.CustomerUpdateModel>();

        }

    }
}
