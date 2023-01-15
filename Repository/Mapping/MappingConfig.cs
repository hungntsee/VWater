using Mapster;
using Repository.Entity;
using Repository.Model.Account;
using Repository.Model.Goods;

namespace Repository.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Account, AccountRequest>();
        config.NewConfig<Account, AccountUpdateRequest>();
        config.NewConfig<Account, AccountRespone>().Map(ar => ar.RoleName, a => a.Role.RoleName);
        config.NewConfig<Account, LoginRequest>();

        config.NewConfig<Goods, GoodsRequest>();
        config.NewConfig<Goods, GoodsUpdateRequest>();
        config.NewConfig<Goods, GoodsResponse>().Map(br => br.BrandName, b => b.BrandVirtutal);
    }
}
