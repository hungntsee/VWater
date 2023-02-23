using System.Text.Json.Serialization;
using VWater.Data;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var service = builder.Services;
        service.AddDbContext<VWaterContext>();
        service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        service.AddControllers().AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });
        var app = builder.Build();

    }
}