using Service.Account;
using Service.Good;
using Service.GoodExchangeNote;
using Service.GoodsCompositions;
using Service.GoodsInBaselines;
using Service.GoodsInProducts;
using Service.GoodsInQuotations;
using Service.Helpers;
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
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGoodsService, GoodsService>();
builder.Services.AddScoped<IGoodsCompositionService, GoodsCompositionService>();
builder.Services.AddScoped<IGoodsExchangeNoteService, GoodsExchangeNoteService>();
builder.Services.AddScoped<IGoodsInBaselineService, GoodsInBaselineService>();
builder.Services.AddScoped<IGoodsInProductService, GoodsInProductService>();
builder.Services.AddScoped<IGoodsInQuotationService, GoodsInQuotationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
