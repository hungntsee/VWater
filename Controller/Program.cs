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
using Service.Distributors;
using Service.Good;
using Service.GoodExchangeNote;
using Service.GoodsCompositions;
using Service.GoodsInBaselines;
using Service.GoodsInProducts;
using Service.GoodsInQuotations;
using Service.Helpers;
using Service.Manufacturers;
using Service.Quotations;
using Service.Services;
using Service.Stores;
using Service.Warehouses;
using System.Text;
using System.Text.Json.Serialization;
using VWater.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<VWaterContext>(ServiceLifetime.Transient);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSetting>(configuration.GetSection("AppSetting"));
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
builder.Services.AddScoped<IQuotationService, QuotationService>();
builder.Services.AddScoped<IDistributorService, DistributorService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IPurchaseOrderDetailService, PurchaseOrderDetailService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountRoleService, AccountRoleService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vwater.API.Integration v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.Run();
