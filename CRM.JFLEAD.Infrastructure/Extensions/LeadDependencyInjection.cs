using CRM.JFLEAD.App;
using CRM.JFLEAD.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.JFLEAD.Infrastructure
{
    public static class LeadDependencyInjection
    {
        public static IServiceCollection AddLeadService(this IServiceCollection services
            , IConfiguration configuration)
        {
            // Add services to the container.
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<DataManagement>();

            return services;
        }
    }
}
