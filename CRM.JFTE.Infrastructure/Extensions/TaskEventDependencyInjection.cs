using CRM.JFTE.App;
using CRM.JFTE.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.JFTE.Infrastructure
{
    public static class TaskEventDependencyInjection
    {
        public static IServiceCollection AddTaskEventServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with the DI container using the connection string from configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register TaskEvent repository and service with the DI container
            services.AddScoped<ITaskEventRepository, TaskEventRepository>();
            services.AddScoped<ITaskEventService, TaskEventService>();

            return services;
        }
    }
}
