using CRM.JFCT.App;
using CRM.JFCT.Core;
using CRM.JFCT.Domain;
using CRM.SharedKernel.App;
using CRM.SharedKernel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.JFCT.Infrastructure
{
    public static class ContactDependencyInjection
    {
        public static IServiceCollection AddContactService(this IServiceCollection services
            , IConfiguration configuration)
        {
            // Add services to the container.
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<Contact, AppDbContext>), typeof(GenericRepository<Contact, AppDbContext>));
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<DataManagement>();

            return services;
        }
    }
}
