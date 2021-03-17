using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Server.Core.Services;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Shared.UnitTests.Services {
    public class CoursesImporterServiceTests {
        [Fact]
        public async Task ImportCourses_ShouldImport_WhenCoursesNotFound() {
            Course c1 = new Course() {
                Code = "CNETIN", Duration = 5, Title = "C# Programming",
                Editions = new List<Edition>() {
                    new Edition() { StartDate = new DateTime(2018, 10, 15) } ,
                    new Edition() { StartDate = new DateTime(2018, 12, 3) },
                    new Edition() { StartDate = new DateTime(2018, 8, 4) }
                }
            };
            Course c2 = new Course() {
                Code = "JPA", Duration = 5, Title = "Java Something something",
                Editions = new List<Edition>() {
                    new Edition() { StartDate = new DateTime(2018, 10, 15) } ,
                    new Edition() { StartDate = new DateTime(2018, 12, 3) },
                    new Edition() { StartDate = new DateTime(2018, 8, 4) }
                }
            };
            List<Course> toAdd = new List<Course>() { c1, c2 };

            Mock<ICoursesImporterRepository> coursesRepositoryMock = new();
            coursesRepositoryMock.Setup(crm => crm.GetCourseWithEditions(It.IsAny<Course>())).Returns(Task.FromResult<Course>(null));
            CoursesImporterService coursesImporterService = new CoursesImporterService(coursesRepositoryMock.Object);

            
            ImportCoursesReply reply = await coursesImporterService.ImportCourses(toAdd);

            Assert.Equal(2, reply.CoursesAdded);
            Assert.Equal(6, reply.EditionsAdded);
        }

        [Fact]
        public async Task ImportCourses_ShouldNotImportDuplicates() {
            Course ec1 = new Course() {
                Id = 1, Code = "EX01", Duration = 5, Title = "ExistingCourse01",
                Editions = new List<Edition>() {
                    new Edition() { Id = 1, CourseId = 1, StartDate = new DateTime(2001, 01, 01) }
                }
            };
            Course ec2 = new Course() {
                Id = 2, Code = "EX02", Duration = 3, Title = "ExistingCourse02",
                Editions = new List<Edition>() {
                    new Edition() {Id = 2, CourseId = 2, StartDate = new DateTime(2002, 02, 02) }
                }
            };

            Course ac1 = new Course() {
                Code = "EX01", Duration = 5, Title = "ExistingCourse01",
                Editions = new List<Edition>() {
                    new Edition() { Id = 1, CourseId = 1, StartDate = new DateTime(2001, 01, 01) } ,
                    new Edition() { Id = 3, CourseId = 1, StartDate = new DateTime(2001, 01, 02) } ,
                    new Edition() { Id = 4, CourseId = 1, StartDate = new DateTime(2001, 01, 03) }
                }
            };

            Course ac2 = new Course() {
                Code = "NOT01", Duration = 5, Title = "NonExistingCourse01",
                Editions = new List<Edition>() {
                    new Edition() { StartDate = new DateTime(2003, 03, 01) } ,
                    new Edition() { StartDate = new DateTime(2003, 03, 02) },
                    new Edition() { StartDate = new DateTime(2003, 03, 03) }
                }
            };

            Mock<ICoursesImporterRepository> coursesRepositoryMock = new();
            coursesRepositoryMock.Setup(crm => crm.GetCourseWithEditions(ac1)).ReturnsAsync(ec1);
            coursesRepositoryMock.Setup(crm => crm.GetCourseWithEditions(ac2)).Returns(Task.FromResult<Course>(null));
            CoursesImporterService coursesImporterService = new CoursesImporterService(coursesRepositoryMock.Object);

            List<Course> toAdd = new List<Course>() { ac1, ac2 };

            ImportCoursesReply reply = await coursesImporterService.ImportCourses(toAdd);

            Assert.Equal(1, reply.CoursesAdded);
            Assert.Equal(5, reply.EditionsAdded);
            
        }
    }
}
