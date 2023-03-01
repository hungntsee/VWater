namespace VWater.Domain.Mapping
{
    public partial class GoodsProfile
        : AutoMapper.Profile
    {
        public GoodsProfile()
        {
            CreateMap<VWater.Data.Entities.Goods, VWater.Domain.Models.GoodsReadModel>();

            CreateMap<VWater.Domain.Models.GoodsCreateModel, VWater.Data.Entities.Goods>();

            CreateMap<VWater.Data.Entities.Goods, VWater.Domain.Models.GoodsUpdateModel>();

            CreateMap<VWater.Domain.Models.GoodsUpdateModel, VWater.Data.Entities.Goods>();

            CreateMap<VWater.Domain.Models.GoodsReadModel, VWater.Domain.Models.GoodsUpdateModel>();

        }

    }
}
