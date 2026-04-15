using App.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Core.DataBaseContext.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<TUser>
{
    public void Configure(EntityTypeBuilder<TUser> builder)
    {
        builder.ToTable("users", schema: "user_credential");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.Username)
            .HasColumnName("username")
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasColumnName("password")
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamptz");

    }
}