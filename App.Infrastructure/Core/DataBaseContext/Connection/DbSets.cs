using App.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Core.DataBaseContext.Connection;

public partial class AppDataBaseContext
{
    public DbSet<TUser> Users { get; set; }
}