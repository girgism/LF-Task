using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.app.ServicesDependencyInjection
{
    public static class ServicesDependencyInjection
    {
        public static IServiceCollection AddServicesForApplicationLayer(this IServiceCollection services)
        {

            var currentAssembly = typeof(ServicesDependencyInjection).Assembly;

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(currentAssembly);
            });

            return services;
        }
    }
}
