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
            
        var dataSource = dataSourceBuilder.Build();
        services.AddSingleton(dataSource);
        services.AddSingleton<DomainEventDispatcherInterceptor>();
        services.AddDbContextPool<AppDataBaseContext>((_, options) =>
        {
            var ds = _.GetRequiredService<Npgsql.NpgsqlDataSource>();
            var interceptor = _.GetRequiredService<DomainEventDispatcherInterceptor>();

            options.AddInterceptors(interceptor);
            options.UseNpgsql(ds, o => { });
        });
        
        #endregion
        
        services.AddAdapterDependencies();
        services.AddCoreServices(configuration);
        
        return services;
    }

}