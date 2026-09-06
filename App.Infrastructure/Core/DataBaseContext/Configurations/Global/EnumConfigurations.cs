using App.Shared.Objects.Enums;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Core.DataBaseContext.Configurations.Global;

public static class EnumConfigurations
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<UserRole>(schema: "auth", name: "user_role_enum");
        modelBuilder.HasPostgresEnum<UserStatus>(schema: "auth", name: "user_status_enum");
        modelBuilder.HasPostgresEnum<VerificationType>(schema: "user_credential", name: "verification_type_enum");
        modelBuilder.HasPostgresEnum<NotificationType>(schema: "notifications", name: "notification_type_enum");
    }
}