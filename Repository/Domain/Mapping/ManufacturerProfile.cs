namespace VWater.Domain.Mapping
{
    public partial class ManufacturerProfile
        : AutoMapper.Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<VWater.Data.Entities.Manufacturer, VWater.Domain.Models.ManufacturerReadModel>();

            CreateMap<VWater.Domain.Models.ManufacturerCreateModel, VWater.Data.Entities.Manufacturer>();

            CreateMap<VWater.Data.Entities.Manufacturer, VWater.Domain.Models.ManufacturerUpdateModel>();

            CreateMap<VWater.Domain.Models.ManufacturerUpdateModel, VWater.Data.Entities.Manufacturer>();

            CreateMap<VWater.Domain.Models.ManufacturerReadModel, VWater.Domain.Models.ManufacturerUpdateModel>();

        }

    }
}
