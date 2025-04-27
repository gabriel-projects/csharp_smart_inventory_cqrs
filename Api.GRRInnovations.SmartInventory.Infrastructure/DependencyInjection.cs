using Api.GRRInnovations.SmartInventory.Infrastructure.Helpers;
using Api.GRRInnovations.SmartInventory.Infrastructure.Persistence;
using Api.GRRInnovations.SmartInventory.Infrastructure.Persistence.Interceptors;
using Api.GRRInnovations.SmartInventory.Infrastructure.Persistence.Repositories;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEFSecondLevelCache(options =>
            {
                options.UseMemoryCacheProvider() // ou Redis, se quiser
                       .ConfigureLogging()
                       .CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(5));
            });

            services.AddSingleton<SecondLevelCacheInterceptor>();

            var connection = ConnectionHelper.GetConnectionString(configuration);
            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, options) =>
            {
                ConfigureDatabase(options, connection);

                var audit = serviceProvider.GetRequiredService<AuditInterceptor>();

                options.AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());

                options.AddInterceptors(audit);
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddSingleton<AuditInterceptor>();

            return services;
        }

        private static void ConfigureDatabase(DbContextOptionsBuilder options, string connection)
        {
            options
            .UseSqlServer(connection, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null
                );

                sqlOptions.CommandTimeout(60);
            });
            
            //.UseLazyLoadingProxies();

#if DEBUG
            options.LogTo(Console.WriteLine, LogLevel.Information)
                   .EnableSensitiveDataLogging(); // CUIDADO: isso mostra dados sensíveis no log
#endif
        }
    }
}
