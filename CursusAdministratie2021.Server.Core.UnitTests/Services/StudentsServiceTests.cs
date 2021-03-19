using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CursusAdministratie2021.Shared.Models;
using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Server.Core.Services;

namespace CursusAdministratie2021.Server.Core.UnitTests.Services {
    public class StudentsServiceTests {
        [Fact]
        public async Task CreateStudent_Should_CreateStudent() {
            Student studentToAdd = new Student() { Name = "StudentName", Surname = "StudentSurname" };
            Student expectedAddedStudent = new Student() { Id = 1, Name = "StudentName", Surname = "StudentSurname", Editions = new List<Edition>()  };
            Mock<IStudentsRepository> studentsRepositoryMock = new();
            studentsRepositoryMock.Setup(sr=>sr.CreateStudent(studentToAdd)).ReturnsAsync(expectedAddedStudent);

            StudentsService studentsService = new StudentsService(studentsRepositoryMock.Object);

            Student actualAddedStudent = await studentsService.CreateStudent(studentToAdd);

            Assert.Equal(expectedAddedStudent, actualAddedStudent);
        }

        [Fact]
        public async Task GetStudent_Should_GetStudent() {
            int studentId = 1;
            Student expectedStudent = new Student() { Id = studentId, Name = "StudentName", Surname = "StudentSurname", Editions = new List<Edition>() };
            Mock<IStudentsRepository> studentsRepositoryMock = new();
            studentsRepositoryMock.Setup(sr => sr.GetStudent(studentId)).ReturnsAsync(expectedStudent);

            StudentsService studentsService = new StudentsService(studentsRepositoryMock.Object);

            Student actualStudent = await studentsService.GetStudent(studentId);

            Assert.Equal(expectedStudent, actualStudent);
        }
    }
}
