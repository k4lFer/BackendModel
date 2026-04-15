using App.Infrastructure.Adapters.User;
using App.Interfaces.Ports;
using App.Interfaces.Ports.User;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Adapters;

public static class AdapterDependencyInjection 
{
    public static IServiceCollection AddAdapterDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
            
        return services;
    }
}