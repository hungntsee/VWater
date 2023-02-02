
using VWater.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var service = builder.Services;
        service.AddDbContext<VWaterContext>();
        service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        var app = builder.Build();
      
    }
}