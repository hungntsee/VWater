using System;
using AutoMapper;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace VWater.Domain.Mapping
{
    public partial class AccountRoleProfile
        : AutoMapper.Profile
    {
        public AccountRoleProfile()
        {
            CreateMap<VWater.Data.Entities.AccountRole, VWater.Domain.Models.AccountRoleReadModel>();

            CreateMap<VWater.Domain.Models.AccountRoleCreateModel, VWater.Data.Entities.AccountRole>();

            CreateMap<VWater.Data.Entities.AccountRole, VWater.Domain.Models.AccountRoleUpdateModel>();

            CreateMap<VWater.Domain.Models.AccountRoleUpdateModel, VWater.Data.Entities.AccountRole>();

            CreateMap<VWater.Domain.Models.AccountRoleReadModel, VWater.Domain.Models.AccountRoleUpdateModel>();

        }

    }
}
