using App.Infrastructure.Adapters;
using App.Infrastructure.Core.DataBaseContext.Connection;
using App.Infrastructure.Core.DataBaseContext.Interceptors;
using App.Infrastructure.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region DataBase Context
        var connectionString = configuration.GetConnectionString("PostgresSQLConnectionString");
        var dataSourceBuilder = new Npgsql.NpgsqlDataSourceBuilder(connectionString);
        // Enum Map
        //dataSourceBuilder.MapEnum<UserRole>("user_credential.user_role_enum");
            
        var dataSource = dataSourceBuilder.Build();
        services.AddScoped<DomainEventDispatcherInterceptor>();

        services.AddDbContextPool<AppDataBaseContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<DomainEventDispatcherInterceptor>();
            options.AddInterceptors(interceptor);
            
            options.UseNpgsql(dataSource, o =>
            {
                // Enum Map
                //o.MapEnum<UserRole>("user_role_enum", schemaName:"user_credential");
            });
        });
        
        #endregion
        
        
        services.AddAdapterDependencies();
        services.AddCoreServices(configuration);
        
        return services;
    }

}