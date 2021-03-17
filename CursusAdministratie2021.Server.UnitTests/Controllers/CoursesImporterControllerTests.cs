using CursusAdministratie2021.Server.Controllers;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Server.UnitTests.Controllers {
    public class CoursesImporterControllerTests {
        Mock<ICoursesImporterService> coursesImporterServiceMock;
        public CoursesImporterControllerTests() {
            coursesImporterServiceMock = new();
        }

        [Fact]
        public async Task ImportCourses_ShouldReturn_ServiceAnswer() {
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
            coursesImporterServiceMock.Setup(cis => cis.ImportCourses(coursesToImport)).ReturnsAsync(expectedReply);

            CoursesImporterController coursesImporterController = new CoursesImporterController(coursesImporterServiceMock.Object);

            ImportCoursesReply actualReply = await coursesImporterController.ImportCourses(coursesToImport);

            Assert.Equal(expectedReply, actualReply);

        }
    }
}
