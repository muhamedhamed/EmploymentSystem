using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-N8DUPEC;Initial Catalog=EmploymentDB;Integrated Security=true;TrustServerCertificate=True;");
                //optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=EmploymentDB;User Id=SA;Password=Password123;");
            }
        }

        // Define DbSet properties for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<ApplicationVacancy> ApplicationVacancies { get; set; }

        //need more investigation about the following method
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationVacancyConfiguration());
        }
    }
}


