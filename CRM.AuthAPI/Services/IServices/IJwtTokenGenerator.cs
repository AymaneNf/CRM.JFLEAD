using Microsoft.AspNetCore.Identity;

namespace CRM.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser user);
    }
}
