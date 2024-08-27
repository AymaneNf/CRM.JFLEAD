using CRM.JFPP.App;
using CRM.JFPP.Core;
using CRM.JFPP.Domain;
using CRM.SharedKernel.App;
using CRM.SharedKernel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.JFPP.Infrastructure
{
    public static class ProspectDependencyInjection
    {
        public static IServiceCollection AddProspectService(this IServiceCollection services
            , IConfiguration configuration)
        {
            // Add services to the container.
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<Prospect,AppDbContext>), typeof(GenericRepository<Prospect,AppDbContext>));
            services.AddScoped<IProspectRepository, ProspectRepository>();
            services.AddScoped<IProspectService, ProspectService>();
            services.AddScoped<DataManagement>();

            return services;
        }
    }
}
