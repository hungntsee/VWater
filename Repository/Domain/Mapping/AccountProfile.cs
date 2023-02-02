using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class AccountProfile
        : AutoMapper.Profile
    {
        public AccountProfile()
        {
            CreateMap<VWater.Data.Entities.Account, VWater.Domain.Models.AccountReadModel>();

            CreateMap<VWater.Domain.Models.AccountCreateModel, VWater.Data.Entities.Account>();

            CreateMap<VWater.Data.Entities.Account, VWater.Domain.Models.AccountUpdateModel>();

            CreateMap<VWater.Domain.Models.AccountUpdateModel, VWater.Data.Entities.Account>();

            CreateMap<VWater.Domain.Models.AccountReadModel, VWater.Domain.Models.AccountUpdateModel>();

        }

    }
}
