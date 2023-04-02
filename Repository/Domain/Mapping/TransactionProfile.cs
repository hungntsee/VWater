namespace VWater.Domain.Mapping
{
    public class TransactionProfile
        : AutoMapper.Profile
    {
        public TransactionProfile()
        {
            CreateMap<VWater.Domain.Models.TransactionCreateModel, VWater.Data.Entities.Transaction>();
        }
    }
}
