using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploymentSystem.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Primary Key configuration
        builder.HasKey(u => u.UserId);

        // Auto-generation of User Id
        builder.Property(u => u.UserId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()")
            .IsUnicode(false)
            .HasMaxLength(36); // Assuming Guid as string has max length of 36

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255); // Adjust the length as needed

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(255); // Adjust the length as needed
    }
}
