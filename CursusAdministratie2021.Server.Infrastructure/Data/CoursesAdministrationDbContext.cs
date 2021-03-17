using CursusAdministratie2021.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//Add-Migration InitialCreate -Project CursusAdministratie2021.Server.Infrastructure -StartupProject CursusAdministratie2021.Server
//Update-Database -Project CursusAdministratie2021.Server.Infrastructure -StartupProject CursusAdministratie2021.Server

namespace CursusAdministratie2021.Server.Infrastructure.Data {
    public class CoursesAdministrationDbContext : DbContext {
        public CoursesAdministrationDbContext(DbContextOptions<CoursesAdministrationDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Course>(ConfigureCourse);
        }
        private void ConfigureCourse(EntityTypeBuilder<Course> builder) {
            builder.Property(p => p.Title)
               .IsRequired(true)
               .HasMaxLength(300);
            builder.Property(p => p.Code)
               .IsRequired(true)
               .HasMaxLength(10);
        }

        //private void ConfigureEnroll(EntityTypeBuilder<Enroll> builder) {
        //    builder.ToTable("Enrolls");

        //    builder
        //        .HasKey(e => new { e.EditionId, e.StudentId });

        //    builder
        //        .HasOne(e => e.Student)
        //        .WithMany(s => s.Enrolls)
        //        .HasForeignKey(e => e.StudentId);

        //    builder
        //        .HasOne(e => e.Edition)
        //        .WithMany(ed => ed.Enrolls)
        //        .HasForeignKey(e => e.EditionId);
        //}
        //private void ConfigureCourse(EntityTypeBuilder<Course> builder) {
        //    builder.ToTable("Courses");

        //    builder.Property(p => p.Id)
        //        .UseHiLo("photos_hilo")
        //        .IsRequired();

        //    builder.Property(p => p.Title)
        //        .IsRequired(true)
        //        .HasMaxLength(255);

        //    builder.Ignore(p => p.ImageUrl);

        //    builder.HasOne(p => p.PhotoImage)
        //        .WithOne()
        //        .HasForeignKey<PhotoImage>(p => p.Id);
        //}
        //private void ConfigureEdition(EntityTypeBuilder<Edition> builder) {
        //    builder.ToTable("Editions");

        //    builder.Property(p => p.Id)
        //        .UseHiLo("photos_hilo")
        //        .IsRequired();

        //    builder.Property(p => p.PhotoFile)
        //        .IsRequired(true);

        //    builder.Property(p => p.ImageMimeType)
        //        .IsRequired(true)
        //        .HasMaxLength(255);
        //}

        //private void ConfigureComment(EntityTypeBuilder<Comment> builder) {
        //    builder.ToTable("Comments");

        //    builder.HasKey(c => c.Id);

        //    builder.Property(c => c.Id)
        //        .UseHiLo("comments_hilo")
        //       .IsRequired();

        //    builder.Property(c => c.Subject)
        //        .IsRequired()
        //        .HasMaxLength(250);

        //    builder.HasOne(c => c.Photo)
        //        .WithMany(p => p.Comments)
        //        .HasForeignKey(c => c.PhotoId);

        //    builder.Property(c => c.PhotoId).IsRequired();
        //}

        public DbSet<Course> Courses { get; set; }
        public DbSet<Edition> Editions { get; set; }
    }
}
