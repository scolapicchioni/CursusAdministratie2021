using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursusAdministratie2021.Server.Infrastructure.Data;
using CursusAdministratie2021.Server.Infrastructure.Repositories;
using CursusAdministratie2021.Shared.Models;
using Moq;
using Xunit;

namespace CursusAdministratie2021.Server.Infrastructure.UnitTests.Repositories {
    public class StudentsRepositoryEFTests : RepositoryEFBase {
        [Fact(Skip = "In Memory DataBase does not support many to many relationships")]
        public async Task CreateStudentShouldAddStudent() {
            Student studentToAdd = new Student() { Name = "StudentName", Surname = "StudentSurname" };

            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                StudentsRepositoryEF studentsRepositoryEF = new(context);
                Student actualAddedStudent = await studentsRepositoryEF.CreateStudent(studentToAdd);

                var expectedAddedStudent = context.Set<Student>().FirstOrDefault(s => s.Name == studentToAdd.Name && s.Surname == studentToAdd.Surname);
                Assert.NotNull(expectedAddedStudent);
                Assert.Equal(expectedAddedStudent, actualAddedStudent);
            }

        }

        [Fact(Skip = "In Memory DataBase does not support many to many relationships")]
        public async Task GetStudentByNameSurname_ShouldReturnStudent_WhenStudentFound() {
            string name = "StudentName";
            string surname = "StudentSurname";

            Student s1 = new Student() {
                Name = name, Surname= surname,
                Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2001, 02, 02) }
                }
            };
            Student s2 = new Student() {
                Name = "Jack", Surname = "Black",
                Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2002, 02, 02) }
                }
            };
            Student s3 = new Student() {
                Name = "Jane", Surname = "Doe",
                Editions = new List<Edition>() {
                        new Edition() { StartDate = new DateTime(2001, 02, 02) }
                }
            };
            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Students.AddRange(s1,s2,s3);
                await context.SaveChangesAsync();
            }

            using (var context = new CoursesAdministrationDbContext(ContextOptions)) {
                StudentsRepositoryEF studentsRepositoryEF = new(context);
                List<Student> studentsFound = await studentsRepositoryEF.FindStudentsBy(name, surname);
                Assert.NotNull(studentsFound);
                Assert.Collection(studentsFound, s => {
                    Assert.Equal(s.Name, s1.Name);
                    Assert.Equal(s.Surname, s1.Surname);
                    Assert.Equal(s.Editions.First().StartDate, s1.Editions.First().StartDate);
                });
            }

        }
    }
}
