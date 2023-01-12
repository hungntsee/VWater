using Repository;
using Mapster;
using Repository.Entity;
using Repository.Model.Account;
using Repository.Mapping;

var builder = WebApplication.CreateBuilder(args);


var service = builder.Services;
service.AddMappings();
service.AddDbContext<DBContext>();

var app = builder.Build();
