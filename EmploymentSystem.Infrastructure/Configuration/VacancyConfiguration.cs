using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure;

public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vacancy> builder)
    {
                // Primary key
        builder.HasKey(v => v.VacancyId);

        // Auto-generate Guid as string
        builder.Property(v => v.VacancyId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        // Other property configurations
        builder.Property(v => v.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(v => v.Description)
            .IsRequired();

        builder.Property(v => v.MaxApplications)
            .IsRequired();

        builder.Property(v => v.ExpiryDate)
            .IsRequired();

        // Relationships if any
        builder.HasOne(v => v.Employer)
            .WithMany(e => e.CreatedVacancies)
            .HasForeignKey(v => v.EmployerId)
            .IsRequired();
    }
}
