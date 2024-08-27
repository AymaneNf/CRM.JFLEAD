using CRM.JFCOM.App;
using CRM.JFCOM.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.JFCOM.Infrastructure
{
    public static class CommercialeDependencyInjection
    {
        public static IServiceCollection AddCommercialeServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with the DI container using the connection string from configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories and services with the DI container
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IDevisRepository, DevisRepository>();
            services.AddScoped<IBonDeCommandeRepository, BonDeCommandeRepository>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IDevisService, DevisService>();
            services.AddScoped<IBonDeCommandeService, BonDeCommandeService>();

            return services;
        }
    }
}
