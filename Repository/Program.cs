using Repository;
using Mapster;
using Repository.Entity;
using Repository.Model.Account;

var builder = WebApplication.CreateBuilder(args);


var service = builder.Services;
service.AddDbContext<DBContext>();

var config = new TypeAdapterConfig();
config.NewConfig<Account, AccountRequest>();
config.NewConfig<Account, AccessRequest>();

var app = builder.Build();

app.Run();
