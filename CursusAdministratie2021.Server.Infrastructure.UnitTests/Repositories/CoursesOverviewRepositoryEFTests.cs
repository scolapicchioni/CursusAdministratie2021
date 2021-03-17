using CursusAdministratie2021.Server.Infrastructure.Data;
using CursusAdministratie2021.Server.Infrastructure.Repositories;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Server.Infrastructure.UnitTests.Repositories {
    public class CoursesOverviewRepositoryEFTests : IDisposable {
        private readonly DbConnection _connection;
        public CoursesOverviewRepositoryEFTests() {
            ContextOptions = new DbContextOptionsBuilder<CoursesAdministrationDbContext>()
                    .UseSqlite(CreateInMemoryDatabase())
                    .Options;
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
            //Seed();
        }
        private static DbConnection CreateInMemoryDatabase() {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();
        protected DbContextOptions<CoursesAdministrationDbContext> ContextOptions { get; }

        [Fact]
        public async Task GetCoursesOverview_ShouldReturnCoursesOverview() {
            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Course pc1 = new Course() {
                    Code = "EX01", Duration = 1, Title = "ExistingCourse01",
                    Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2020, 06, 15) }
                    }
                };
                Course pc2 = new Course() {
                    Code = "EX02", Duration = 3, Title = "ExistingCourse02",
                    Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2002, 02, 02) }
                    }
                };

                context.Courses.AddRange(pc1, pc2);
                await context.SaveChangesAsync();
            }

            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                CoursesOverviewRepositoryEF repository = new CoursesOverviewRepositoryEF(context);
                IEnumerable<CourseOverview> courses = await repository.GetCoursesOverview();
                Assert.Collection(courses,
                    c => {
                        Assert.Equal("ExistingCourse01", c.Title);
                        Assert.Equal(1, c.Duration);
                        Assert.Equal(new DateTime(2020, 06, 15), c.StartDate);
                    },
                    c => {
                        Assert.Equal("ExistingCourse02", c.Title);
                        Assert.Equal(3, c.Duration);
                        Assert.Equal(new DateTime(2002, 02, 02), c.StartDate);
                    });
            }
        }
    }
}
