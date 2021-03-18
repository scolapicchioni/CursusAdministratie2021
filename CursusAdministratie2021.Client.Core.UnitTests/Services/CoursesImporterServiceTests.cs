using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursusAdministratie2021.Client.Core.Interfaces;
using CursusAdministratie2021.Client.Core.Services;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using Moq;
using Xunit;

namespace CursusAdministratie2021.Client.Core.UnitTests.Services {
    public class CoursesImporterServiceTests {
        [Fact]
        public async Task ImportCourses_ShouldReturn_ImportCourseReply() {
            List<Course> coursesToImport = new List<Course> {
                new Course(){
                    Code = "CNETIN", Duration = 5, Title = "C# Programming",
                    Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2018, 10, 15) } ,
                        new Edition() { StartDate = new DateTime(2018, 12, 3) },
                        new Edition() { StartDate = new DateTime(2018, 8, 4) }
                    }
                },
                new Course() {
                    Code = "JPA", Duration = 5, Title = "Java Something something",
                    Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2018, 10, 15) } ,
                        new Edition() { StartDate = new DateTime(2018, 12, 3) },
                        new Edition() { StartDate = new DateTime(2018, 8, 4) }
                    }
                }
            };
            ImportCoursesReply expectedReply = new ImportCoursesReply() { CoursesAdded = 1, EditionsAdded = 5 };
            Mock<ICoursesImporterRepository> coursesImporterRepositoryMock = new();
            coursesImporterRepositoryMock.Setup(cir => cir.ImportCourses(coursesToImport)).ReturnsAsync(expectedReply);

            CoursesImporterService coursesImporterService = new(coursesImporterRepositoryMock.Object);
            ImportCoursesReply actualReply = await coursesImporterService.ImportCourses(coursesToImport);

            coursesImporterRepositoryMock.Verify(cir => cir.ImportCourses(coursesToImport));
            Assert.Equal(expectedReply, actualReply);
        }
    }
}
