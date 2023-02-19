using Service.Account;
using Service.Helpers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// configure strongly typed settings object
services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();