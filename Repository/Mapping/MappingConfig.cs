using Mapster;
using Repository.Entity;
using Repository.Model.Account;

namespace Repository.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Account, AccountRequest>();
        config.NewConfig<Account, AccountUpdateRequest>();
        config.NewConfig<Account, AccountRespone>().Map(ar => ar.RoleName, a => a.Role.RoleName);
        config.NewConfig<Account, LoginRequest>();
    }
}
