using Api.GRRInnovations.SmartInventory.Application.Services;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using Api.GRRInnovations.SmartInventory.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.GRRInnovations.SmartInventory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                //config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                //config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            return services;
        }
    }
}
