using CursusAdministratie2021.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//Add-Migration InitialCreate -Project CursusAdministratie2021.Server.Infrastructure -StartupProject CursusAdministratie2021.Server
//Update-Database -Project CursusAdministratie2021.Server.Infrastructure -StartupProject CursusAdministratie2021.Server

//Add-Migration Student -Project CursusAdministratie2021.Server.Infrastructure -StartupProject CursusAdministratie2021.Server

//Add-Migration StudentHierarchy -Project CursusAdministratie2021.Server.Infrastructure -StartupProject CursusAdministratie2021.Server

namespace CursusAdministratie2021.Server.Infrastructure.Data {
    public class CoursesAdministrationDbContext : DbContext {
        public CoursesAdministrationDbContext(DbContextOptions<CoursesAdministrationDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Course>(ConfigureCourse);
            modelBuilder.Entity<Student>(ConfigureStudent);
            modelBuilder.Entity<CompanyEmployee>(ConfigureCompanyEmployee);
            modelBuilder.Entity<PrivateCitizen>(ConfigurePrivateCitizen);

            modelBuilder
                .Entity<Student>()
                .HasMany(s => s.Editions)
                .WithMany(e => e.Students)
                .UsingEntity(j => j.ToTable("Enrollments"));
        }
        private void ConfigureCourse(EntityTypeBuilder<Course> builder) {
            builder.Property(p => p.Title)
               .IsRequired(true)
               .HasMaxLength(300);
            builder.Property(p => p.Code)
               .IsRequired(true)
               .HasMaxLength(10);
        }

        private void ConfigureStudent(EntityTypeBuilder<Student> builder) {
            builder.Property(p => p.Name)
               .IsRequired(true)
               .HasMaxLength(200);
            builder.Property(p => p.Surname)
               .IsRequired(true)
               .HasMaxLength(200);
        }

        private void ConfigureCompanyEmployee(EntityTypeBuilder<CompanyEmployee> builder) {
            builder.Property(p => p.Company)
                .IsRequired(true)
               .HasMaxLength(200);
            builder.Property(p => p.Department)
               .HasMaxLength(200);
            builder.Property(p => p.Quotation)
               .HasMaxLength(30);
        }

        private void ConfigurePrivateCitizen(EntityTypeBuilder<PrivateCitizen> builder) {
            builder.Property(p => p.StreetName)
               .HasMaxLength(200);
            builder.Property(p => p.HouseNumber)
               .HasMaxLength(6);
            builder.Property(p => p.ZipCode)
               .HasMaxLength(30);
            builder.Property(p => p.City)
               .HasMaxLength(100);
            builder.Property(p => p.IBAN)
               .HasMaxLength(40);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<PrivateCitizen> PrivateCitizens { get; set; }
    }
}
