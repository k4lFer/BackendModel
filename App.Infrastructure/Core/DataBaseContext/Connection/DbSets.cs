using App.Domain.User.Entities;
using App.Infrastructure.Core.DataBaseContext.Audit;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Core.DataBaseContext.Connection;

public partial class AppDataBaseContext
{
    public DbSet<TUser> Users { get; set; }
    public DbSet<TPerson> Persons { get; set; }
    public DbSet<TRefreshToken> RefreshTokens { get; set; }
    public DbSet<TUserGateway> UserGateways { get; set; }
    public DbSet<AuditMessage> AuditMessages { get; set; }
}
