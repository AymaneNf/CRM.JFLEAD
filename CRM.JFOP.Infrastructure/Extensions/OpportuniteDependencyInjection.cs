using CRM.JFOP.App;
using CRM.JFOP.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.JFOP.Infrastructure
{
    public static class OpportuniteDependencyInjection
    {
        public static IServiceCollection AddOpportuniteServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with the DI container using the connection string from configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register Opportunite repository and service with the DI container
            services.AddScoped<IOpportuniteRepository, OpportuniteRepository>();
            services.AddScoped<IOpportuniteService, OpportuniteService>();

            return services;
        }
    }
}
