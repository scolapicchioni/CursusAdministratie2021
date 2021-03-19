using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Server.Core.Services;
using CursusAdministratie2021.Shared.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Server.Core.UnitTests.Services {
    public class EditionsServiceTests {
        Mock<IEditionsRepository> editionRepositoryMock;
        EditionsService editionsService;
        public EditionsServiceTests() {
            editionRepositoryMock = new();
            editionsService = new(editionRepositoryMock.Object);
        }
        [Fact]
        public async Task GetEditionWithStudents_ShouldReturnEditionWithStudents_WhenEditionFound() {
            Edition expectedEdition = new Edition() {
                Id = 1, CourseId = 1, StartDate = new DateTime(2021, 01, 01), Students = new List<Student>() {
                    new Student(){ Id = 1, Name = "StudentName", Surname = "StudentSurname" }
                }
            };
            editionRepositoryMock.Setup(er => er.GetEditionWithStudents(1)).ReturnsAsync(expectedEdition);
            Edition actualEdition = await editionsService.GetEditionWithStudents(1);
            Assert.Equal(expectedEdition, actualEdition);
        }
    }
}
