namespace VWater.Domain.Mapping
{
    public class WalletProfile
        : AutoMapper.Profile
    {
        public WalletProfile()
        {
            CreateMap<VWater.Domain.Models.WalletCreateModel, VWater.Data.Entities.Wallet>();
        }
    }
}
