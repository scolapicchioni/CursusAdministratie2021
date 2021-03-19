using CursusAdministratie2021.Server.Infrastructure.Data;
using CursusAdministratie2021.Server.Infrastructure.Repositories;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Server.Infrastructure.UnitTests.Repositories {
    public class CoursesImporterRepositoryEFTests : RepositoryEFBase {
        
        public CoursesImporterRepositoryEFTests() {
        
        }
        
        [Fact]
        public async Task GetCourseWithEdition_ShouldReturnExistingCourse() {
            Course pc1 = new Course() {
                Code = "EX01", Duration = 1, Title = "ExistingCourse01",
                Editions = new List<Edition>() {
                    new Edition() { StartDate = new DateTime(2020, 06, 15) }
                }
            };
            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
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
                CoursesImporterRepositoryEF repository = new CoursesImporterRepositoryEF(context);
                Course course = await repository.GetCourseWithEditions(pc1);
                Assert.Equal(pc1.Id, course.Id);
                Assert.Collection(pc1.Editions, e => Assert.Equal(1, e.Id));
            }
        }

        [Fact]
        public async Task GetCourseWithEdition_ShouldReturnNull_WhenCourseDoesNotExist() {
            Course pc1 = new Course() {
                Code = "EX01", Duration = 1, Title = "ExistingCourse01",
                Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2020, 06, 15) }
                    }
            };
            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Course pc2 = new Course() {
                    Code = "EX02", Duration = 3, Title = "ExistingCourse02",
                    Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2002, 02, 02) }
                    }
                };

                context.Courses.AddRange(pc2);
                await context.SaveChangesAsync();
            }

            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                CoursesImporterRepositoryEF repository = new CoursesImporterRepositoryEF(context);
                Course course = await repository.GetCourseWithEditions(pc1);
                Assert.Null(course);
            }
        }

    }
}
