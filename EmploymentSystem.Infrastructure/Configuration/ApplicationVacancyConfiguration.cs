using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure;

public class ApplicationVacancyConfiguration : IEntityTypeConfiguration<ApplicationVacancy>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApplicationVacancy> builder)
    {
        // Primary key
        builder.HasKey(a => a.ApplicationVacancyId);

        // Auto-generate Guid as string
        builder.Property(a => a.ApplicationVacancyId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        // Relationships if any
        builder.HasOne(a => a.Applicant)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.ApplicantId)
            .IsRequired();

        builder.HasOne(a => a.AppliedVacancy)
            .WithMany(v => v.Applications)
            .HasForeignKey(a => a.VacancyId)
            .IsRequired();
    }
}
