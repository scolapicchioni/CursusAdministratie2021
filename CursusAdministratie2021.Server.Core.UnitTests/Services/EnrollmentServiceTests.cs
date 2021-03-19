using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.Models;
using System.ComponentModel.DataAnnotations;
using CursusAdministratie2021.Server.Core.Services;

namespace CursusAdministratie2021.Server.Core.UnitTests.Services {
    public class EnrollmentServiceTests {
        EnrollmentService enrollmentService;
        Mock<IStudentsService> studentsServiceMock;
        Mock<IEditionsService> editionsServiceMock;
        Mock<IEnrollmentRepository> enrollmentRepositoryMock;

        public EnrollmentServiceTests() {
            studentsServiceMock = new();
            editionsServiceMock = new();
            enrollmentRepositoryMock = new();
            enrollmentService = new(studentsServiceMock.Object, editionsServiceMock.Object, enrollmentRepositoryMock.Object);
        }
        [Fact]
        public async Task EnrollStudentToEdition_ShouldThrow_WhenEditionNotFound() {
            int editionId = 1;
            int studentId = 1;
            Student student = new Student();
            editionsServiceMock.Setup(es=>es.GetEditionWithStudents(editionId)).Returns(Task.FromResult<Edition>(null));

            await Assert.ThrowsAsync<ValidationException>(async () => {
                await enrollmentService.EnrollStudentToEdition(studentId, editionId);
            });
        }

        [Fact]
        public async Task EnrollStudentToEdition_ShouldThrow_WhenNoSpotsAvailable() {
            int editionId = 1;
            int studentId = 1;
            Edition existingEdition = new Edition(){
                Id = 1, Students = new List<Student>() {
                    new Student(), new Student(), new Student(), 
                    new Student(), new Student(), new Student(), 
                    new Student(), new Student(), new Student(), 
                    new Student(), new Student(), new Student()
                } 
            };
            editionsServiceMock.Setup(es => es.GetEditionWithStudents(editionId)).ReturnsAsync(existingEdition);

            await Assert.ThrowsAsync<ValidationException>(async () => {
                await enrollmentService.EnrollStudentToEdition(studentId, editionId);
            });
        }

        [Fact]
        public async Task EnrollStudentToEdition_ShouldThrow_WhenStudentAlreadyEnrolled() {
            int editionId = 1;
            int studentId = 1;
            Edition existingEdition = new Edition() {
                Id = 1, Students = new List<Student>() {
                    new Student() { Id = 1 }
                }
            };
            editionsServiceMock.Setup(es => es.GetEditionWithStudents(editionId)).ReturnsAsync(existingEdition);

            await Assert.ThrowsAsync<ValidationException>(async () => {
                await enrollmentService.EnrollStudentToEdition(studentId, editionId);
            });
        }

        [Fact]
        public async Task EnrollStudentToEdition_ShouldThrow_WhenStudentNotFound() {
            int editionId = 1;
            int studentId = 1;
            Edition existingEdition = new Edition() {
                Id = 1, Students = new List<Student>() {
                    
                }
            };
            
            editionsServiceMock.Setup(es => es.GetEditionWithStudents(editionId)).ReturnsAsync(existingEdition);
            
            studentsServiceMock.Setup(ss => ss.GetStudent(studentId)).Returns(Task.FromResult<Student>(null));

            await Assert.ThrowsAsync<ValidationException>(async () => {
                await enrollmentService.EnrollStudentToEdition(studentId, editionId);
            });
        }


        [Fact]
        public async Task EnrollStudentToEdition_ShouldEnroll() {
            int editionId = 1;
            int studentId = 1;
            Edition existingEdition = new Edition() {
                Id = 1, Students = new List<Student>() {

                }
            };
            Student student = new Student() {
                Id = 1
            };
            editionsServiceMock.Setup(es => es.GetEditionWithStudents(editionId)).ReturnsAsync(existingEdition);

            studentsServiceMock.Setup(ss => ss.GetStudent(studentId)).ReturnsAsync(student);

            await enrollmentService.EnrollStudentToEdition(studentId, editionId);

            enrollmentRepositoryMock.Verify(er => er.EnrollStudentToEdition(studentId, editionId));
        }

        
    }
}
