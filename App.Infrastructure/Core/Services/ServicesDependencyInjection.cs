using App.Infrastructure.Core.Services.Security;
using App.Interfaces.Common.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Core.Services;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        
        // Register services here
        //services.AddScoped<ITokenProvider, JwtTokenProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}