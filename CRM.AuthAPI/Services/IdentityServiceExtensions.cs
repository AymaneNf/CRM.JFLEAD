using CRM.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace CRM.AuthAPI.Services
{
    public static class IdentityServiceExtensions
    {
        public static void MapIdentityApi<TUser>(this IEndpointRouteBuilder endpoints) where TUser : class
        {
            // Configure the endpoints for the Identity API
            endpoints.MapControllers();
            
        }
    }
}
