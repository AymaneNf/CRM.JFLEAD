using CRM.JFCL.App;
using CRM.JFCL.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.JFCL.Infrastructure
{
    public static class ClientDependencyInjection
    {
        public static IServiceCollection AddClientService(this IServiceCollection services
            , IConfiguration configuration)
        {
            // Add services to the container.
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<DatabaseManagement>();

            return services;
        }
    }
}
