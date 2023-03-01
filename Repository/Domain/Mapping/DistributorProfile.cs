namespace VWater.Domain.Mapping
{
    public partial class DistributorProfile
        : AutoMapper.Profile
    {
        public DistributorProfile()
        {
            CreateMap<VWater.Data.Entities.Distributor, VWater.Domain.Models.DistributorReadModel>();

            CreateMap<VWater.Domain.Models.DistributorCreateModel, VWater.Data.Entities.Distributor>();

            CreateMap<VWater.Data.Entities.Distributor, VWater.Domain.Models.DistributorUpdateModel>();

            CreateMap<VWater.Domain.Models.DistributorUpdateModel, VWater.Data.Entities.Distributor>();

            CreateMap<VWater.Domain.Models.DistributorReadModel, VWater.Domain.Models.DistributorUpdateModel>();

        }

    }
}
