namespace VWater.Domain.Mapping
{
    public partial class TransactionTypeProfile
        : AutoMapper.Profile
    {
        public TransactionTypeProfile()
        {
            CreateMap<VWater.Data.Entities.TransactionType, VWater.Domain.Models.TransactionTypeReadModel>();

            CreateMap<VWater.Domain.Models.TransactionTypeCreateModel, VWater.Data.Entities.TransactionType>();

            CreateMap<VWater.Data.Entities.TransactionType, VWater.Domain.Models.TransactionTypeUpdateModel>();

            CreateMap<VWater.Domain.Models.TransactionTypeUpdateModel, VWater.Data.Entities.TransactionType>();

            CreateMap<VWater.Domain.Models.TransactionTypeReadModel, VWater.Domain.Models.TransactionTypeUpdateModel>();

        }

    }
}
