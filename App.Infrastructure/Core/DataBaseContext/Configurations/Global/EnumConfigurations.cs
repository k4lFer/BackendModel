using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Core.DataBaseContext.Configurations.Global;

public static class EnumConfigurations
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasPostgresEnum<UserRole>(schema: "auth", name: "user_role_enum");
    }
}