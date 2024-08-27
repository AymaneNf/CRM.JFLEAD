using CRM.AuthAPI.Data;
using CRM.AuthAPI.Services; 
using CRM.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity services configuration with roles
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Add roles
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add custom services
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); // Add JwtTokenGenerator service
builder.Services.AddScoped<IAuthService, AuthService>(); // Add AuthService
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IRoleService, RoleService>();

// JWT authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Authorization configuration
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PermissionPolicy", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            var permission = context.Resource as string;
            return user.HasClaim("Permission", permission);
        }));
});

// Add Razor Pages
builder.Services.AddRazorPages();

// Swagger configuration for API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });

    // Define the security scheme for JWT authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Add the security requirement for JWT
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// HTTP request pipeline configuration
if (app.Environment.IsDevelopment())
{
    // Use Swagger in development mode
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at application root
    });
}
else
{
    // Error handling in production
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Serve static files
app.UseStaticFiles();

app.UseRouting();

// Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controllers and Razor Pages
app.MapRazorPages();
app.MapControllers(); // Configure endpoints for controllers
CRM.AuthAPI.Services.IdentityServiceExtensions.MapIdentityApi<IdentityUser>(app); // Explicitly use the custom extension method

// Seed roles (initialization)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

app.Run();
