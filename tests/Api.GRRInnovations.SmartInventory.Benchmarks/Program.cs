using Api.GRRInnovations.SmartInventory.Benchmarks.Services;
using Api.GRRInnovations.SmartInventory.Infrastructure.Persistence;
using Api.GRRInnovations.SmartInventory.Infrastructure.Persistence.Repositories;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api.GRRInnovations.SmartInventory.Benchmarks
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer("SqlConnectionString");
                        options.EnableSensitiveDataLogging();
                    });

                    services.AddTransient<BenchmarkService>();

                    services.AddScoped<IProductRepository, ProductRepository>();
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var benchmark = scope.ServiceProvider.GetRequiredService<BenchmarkService>();
            await benchmark.BenchmarkGetAllWithAndWithoutTracking();
        }
    }
}
