using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace App.Infrastructure.Core.DataBaseContext.Connection;

public class DbContextFactory : IDesignTimeDbContextFactory<AppDataBaseContext>
{
    public AppDataBaseContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("PostgresSQLConnectionString");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'PostgresSQLConnectionString' not found in appsettings.json");
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDataBaseContext>();
        
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();
        
        optionsBuilder.UseNpgsql(dataSource, o =>
        {
            // Configuraciones de Npgsql si las hay
        });

        return new AppDataBaseContext(optionsBuilder.Options);
    }
}
