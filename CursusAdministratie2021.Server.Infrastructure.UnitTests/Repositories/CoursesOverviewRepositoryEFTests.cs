using CursusAdministratie2021.Server.Infrastructure.Data;
using CursusAdministratie2021.Server.Infrastructure.Repositories;
using CursusAdministratie2021.Shared.CalendarHelpers;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Server.Infrastructure.UnitTests.Repositories {
    public class CoursesOverviewRepositoryEFTests : IDisposable {
        private readonly DbConnection _connection;
        Mock<ICalendarHelper> calendarHelperMock;
        public CoursesOverviewRepositoryEFTests() {
            ContextOptions = new DbContextOptionsBuilder<CoursesAdministrationDbContext>()
                    .UseSqlite(CreateInMemoryDatabase())
                    .Options;
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
            calendarHelperMock = new();
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
        public async Task GetCoursesOverview_ShouldReturnCoursesOverview_OrderedByStartDate_AndTitle() {
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
            Course pc3 = new Course() {
                Code = "EX03", Duration = 3, Title = "ExistingCourse03",
                Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2002, 02, 02) }
                    }
            };
            Course pc4 = new Course() {
                Code = "EX04", Duration = 3, Title = "ExistingCourse04",
                Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2021, 02, 02) }
                    }
            };
            Course pc5 = new Course() {
                Code = "EX05", Duration = 3, Title = "ExistingCourse05",
                Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2001, 02, 02) }
                    }
            };
            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Courses.AddRange(pc1, pc2, pc3, pc4, pc5);
                await context.SaveChangesAsync();
            }

            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                CoursesOverviewRepositoryEF repository = new CoursesOverviewRepositoryEF(context, calendarHelperMock.Object);
                IEnumerable<CourseOverview> courses = await repository.GetCoursesOverview();
                Assert.Collection(courses,
                    c => {
                        Assert.Equal(pc5.Title, c.Title);
                        Assert.Equal(pc5.Duration, c.Duration);
                        Assert.Equal(pc5.Editions.First().StartDate, c.StartDate);
                    },
                    c => {
                        Assert.Equal(pc2.Title, c.Title);
                        Assert.Equal(pc2.Duration, c.Duration);
                        Assert.Equal(pc2.Editions.First().StartDate, c.StartDate);
                    },
                    c => {
                        Assert.Equal(pc3.Title, c.Title);
                        Assert.Equal(pc3.Duration, c.Duration);
                        Assert.Equal(pc3.Editions.First().StartDate, c.StartDate);
                    },
                    c => {
                        Assert.Equal(pc1.Title, c.Title);
                        Assert.Equal(pc1.Duration, c.Duration);
                        Assert.Equal(pc1.Editions.First().StartDate, c.StartDate);
                    },
                    c => {
                        Assert.Equal(pc4.Title, c.Title);
                        Assert.Equal(pc4.Duration, c.Duration);
                        Assert.Equal(pc4.Editions.First().StartDate, c.StartDate);
                    });
            }
        }

        [Fact]
        public async Task GetCoursesPerWeek_ShouldReturnCourseInGivenWeek() {
            
            int year = 2020;
            int week = 25;
            calendarHelperMock.Setup(c => c.GetWeekRange(year, week)).Returns((begin: new DateTime(2020, 06, 15), end: new DateTime(2020, 06, 21)));
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
                CoursesOverviewRepositoryEF repository = new CoursesOverviewRepositoryEF(context, calendarHelperMock.Object);
                IEnumerable<CourseOverview> courses = await repository.GetCoursesPerWeek(year, week);
                Assert.Collection(courses,
                c => {
                    Assert.Equal("ExistingCourse01", c.Title);
                    Assert.Equal(1, c.Duration);
                    Assert.Equal(new DateTime(2020, 06, 15), c.StartDate);
                });
            }
        }
    }
}
