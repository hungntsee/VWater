namespace VWater.Domain.Mapping
{
    public class DepositNoteProfile : AutoMapper.Profile
    {
        public DepositNoteProfile()
        {
            CreateMap<VWater.Domain.Models.DepositNoteCreateModel, VWater.Data.Entities.DepositNote>();
        }
    }
}
