using Domain.app.Interfaces;
using Infrastructure.app.DBConfigurations;

namespace ElectronicsApp.Server.ServicesDependencyInjection
{
    public static class ServicesDependencyInjection
    {
        public static IServiceCollection AddAPIDependencyInjection(this IServiceCollection services
            )
        {

            services.AddScoped<ElectronicsContext>();
            services.AddScoped<IElectronicsContext>(op => op.GetService<ElectronicsContext>());
            return services;
        }
    }
}
