﻿using Microsoft.AspNetCore.Identity;

namespace CRM.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string username, string password);
        Task<IdentityResult> RegisterAsync(string username, string password);
    }
}
