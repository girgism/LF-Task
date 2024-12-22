using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.app.DBConfigurations
{
    public static class DatabaseServiceConfiguration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
                                                     IConfiguration configuration)
        {
            services.AddDbContext<ElectronicsContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(configuration.GetConnectionString("DBConnection"));
            });

            return services;
        }
    }
}
