using Controller.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Service.Account;
using Service.Apartments;
using Service.Areas;
using Service.Brands;
using Service.Buildings;
using Service.DeliveryAddresses;
using Service.DeliverySlots;
using Service.DeliveryTypes;
using Service.Good;
using Service.GoodExchangeNote;
using Service.GoodsCompositions;
using Service.GoodsInBaselines;
using Service.GoodsInProducts;
using Service.GoodsInQuotations;
using Service.Helpers;
using Service.Services;
using Service.Stores;
using Service.Warehouses;
using System.Text;
using VWater.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VWaterContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").ToString()))
        };
    });
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGoodsService, GoodsService>();
builder.Services.AddScoped<IGoodsCompositionService, GoodsCompositionService>();
builder.Services.AddScoped<IGoodsExchangeNoteService, GoodsExchangeNoteService>();
builder.Services.AddScoped<IGoodsInBaselineService, GoodsInBaselineService>();
builder.Services.AddScoped<IGoodsInProductService, GoodsInProductService>();
builder.Services.AddScoped<IGoodsInQuotationService, GoodsInQuotationService>();
builder.Services.AddScoped<IBuildingService, BuiildingService>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IDeliveryAddressService, DeliveryAddressService>();
builder.Services.AddScoped<IDeliverySlotService, DeliverySlotService>();
builder.Services.AddScoped<IDeliveryTypeService, DeliveryTypeService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IStoreService, StoreService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.Run();
