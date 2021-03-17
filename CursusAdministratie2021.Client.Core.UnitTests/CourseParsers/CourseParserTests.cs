using CursusAdministratie2021.Client.Core.CourseParsers;
using CursusAdministratie2021.Shared.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Client.Core.UnitTests.CourseParsers {
    public class CourseParserTests {
        [Fact]
        public async Task ShouldImportCorrectFormat() {

            string fakeFileContents =
$@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8/10/2018

Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 15/10/2018

Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 15/10/2018

Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 8/10/2018

Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8/10/2018

";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            List<Course> courses = await sut.ParseAsync(fakeMemoryStream);

            Assert.Collection(courses,
            c => {
                Assert.Equal("C# Programmeren", c.Title);
                Assert.Equal("CNETIN", c.Code);
                Assert.Equal(5, c.Duration);
                Assert.Collection(c.Editions,
                e => {
                    Assert.Equal(new DateTime(2018, 10, 8), e.StartDate);
                },
                e => {
                    Assert.Equal(new DateTime(2018, 10, 15), e.StartDate);
                });
            },
            c => {
                Assert.Equal("Java Persistence API", c.Title);
                Assert.Equal("JPA", c.Code);
                Assert.Equal(2, c.Duration);
                Assert.Collection(c.Editions, e => {
                    Assert.Equal(new DateTime(2018, 10, 15), e.StartDate);
                },
                e => {
                    Assert.Equal(new DateTime(2018, 10, 8), e.StartDate);
                });
            });
        }

        [Fact]
        public async Task ShouldThrowWhenIncorrectOrder() {

            string fakeFileContents =
$@"Titel: C# Programmeren
Duur: 5 dagen
Cursuscode: CNETIN
Startdatum: 8/10/2018

";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(async () => {
                List<Course> courses = await sut.ParseAsync(fakeMemoryStream);
            });

        }


        [Fact]
        public async Task ShouldThrowWhenMissingData() {

            string fakeFileContents =
$@"Titel: C# Programmeren
Cursuscode: CNETIN
Startdatum: 8/10/2018

";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(async () => {
                List<Course> courses = await sut.ParseAsync(fakeMemoryStream);
            });

        }

        [Fact]
        public async Task ShouldThrowWhenDateNotInCorrectFormat() {

            string fakeFileContents =
$@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8-10-2018

";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(async () => {
                List<Course> courses = await sut.ParseAsync(fakeMemoryStream);
            });

        }

        [Fact]
        public async Task ShouldThrowWhenDurationNotInCorrectFormat() {

            string fakeFileContents =
$@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5
Startdatum: 8/10/2018
";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(async () => {
                List<Course> courses = await sut.ParseAsync(fakeMemoryStream);
            });

        }

        [Fact]
        public async Task ShouldThrowWhenMissingLineBetweenCourses() {

            string fakeFileContents =
    $@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8/10/2018
Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 10/10/2018

";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(async () => {
                List<Course> courses = await sut.ParseAsync(fakeMemoryStream);
            });

        }


        private static CourseParser InitCourseImporter(string fakeFileContents) {

            Mock<ICoursePropertyParser<string>> titleParserMock = new Mock<ICoursePropertyParser<string>>();
            Mock<ICoursePropertyParser<string>> codeParserMock = new Mock<ICoursePropertyParser<string>>();
            Mock<ICoursePropertyParser<int>> durationParserMock = new Mock<ICoursePropertyParser<int>>();
            Mock<ICoursePropertyParser<DateTime>> startDateParserMock = new Mock<ICoursePropertyParser<DateTime>>();
            Mock<ICoursePropertyParser<string>> emptyLineParserMock = new Mock<ICoursePropertyParser<string>>();


            titleParserMock.Setup(p => p.Parse("Titel: C# Programmeren")).Returns("C# Programmeren");
            titleParserMock.Setup(p => p.Parse("Titel: Java Persistence API")).Returns("Java Persistence API");
            titleParserMock.Setup(p => p.Parse(It.IsNotIn("Titel: C# Programmeren", "Titel: Java Persistence API"))).Throws<ValidationException>();

            codeParserMock.Setup(p => p.Parse("Cursuscode: CNETIN")).Returns("CNETIN");
            codeParserMock.Setup(p => p.Parse("Cursuscode: JPA")).Returns("JPA");
            codeParserMock.Setup(p => p.Parse(It.IsNotIn("Cursuscode: CNETIN", "Cursuscode: JPA"))).Throws<ValidationException>();


            durationParserMock.Setup(p => p.Parse("Duur: 5 dagen")).Returns(5);
            durationParserMock.Setup(p => p.Parse("Duur: 2 dagen")).Returns(2);
            durationParserMock.Setup(p => p.Parse(It.IsNotIn("Duur: 5 dagen", "Duur: 2 dagen"))).Throws<ValidationException>();

            startDateParserMock.Setup(p => p.Parse("Startdatum: 8/10/2018")).Returns(new DateTime(2018, 10, 8));
            startDateParserMock.Setup(p => p.Parse("Startdatum: 15/10/2018")).Returns(new DateTime(2018, 10, 15));
            startDateParserMock.Setup(p => p.Parse(It.IsNotIn("Startdatum: 8/10/2018", "Startdatum: 15/10/2018"))).Throws<ValidationException>();

            emptyLineParserMock.Setup(p => p.Parse("")).Returns("");

            CourseParser sut = new CourseParser(titleParserMock.Object, codeParserMock.Object, durationParserMock.Object, startDateParserMock.Object, emptyLineParserMock.Object);

            return sut;
        }



        [Fact]
        public async Task ShouldImportCoursesBetweenDates() {

            string fakeFileContents =
$@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8/10/2018

Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 15/10/2018

Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 15/10/2018

Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 8/10/2018

";

            DateTime startDate = new DateTime(2018, 10, 08);
            DateTime endDate = new DateTime(2018, 10, 15);

            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            List<Course> courses = await sut.ParseAsync(fakeMemoryStream, startDate, endDate);

            Assert.Collection(courses,
            c => {
                Assert.Equal("C# Programmeren", c.Title);
                Assert.Equal("CNETIN", c.Code);
                Assert.Equal(5, c.Duration);
                Assert.Collection(c.Editions,
                e => {
                    Assert.Equal(new DateTime(2018, 10, 8), e.StartDate);
                },
                e => {
                    Assert.Equal(new DateTime(2018, 10, 15), e.StartDate);
                });
            },
            c => {
                Assert.Equal("Java Persistence API", c.Title);
                Assert.Equal("JPA", c.Code);
                Assert.Equal(2, c.Duration);
                Assert.Collection(c.Editions, e => {
                    Assert.Equal(new DateTime(2018, 10, 15), e.StartDate);
                },
                e => {
                    Assert.Equal(new DateTime(2018, 10, 8), e.StartDate);
                });
            });
        }

        [Fact]
        public async Task ShouldNotImportCoursesBeginninAfterEndOfRange() {

            string fakeFileContents =
$@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8/10/2018

Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 15/10/2018

Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 15/10/2018

Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 8/10/2018

Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8/10/2018

";

            DateTime startDate = new DateTime(2018, 01, 01);
            DateTime endDate = new DateTime(2018, 10, 10);

            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            List<Course> courses = await sut.ParseAsync(fakeMemoryStream, startDate, endDate);

            Assert.Collection(courses,
            c => {
                Assert.Equal("C# Programmeren", c.Title);
                Assert.Equal("CNETIN", c.Code);
                Assert.Equal(5, c.Duration);
                Assert.Collection(c.Editions,
                e => {
                    Assert.Equal(new DateTime(2018, 10, 8), e.StartDate);
                });
            },
            c => {
                Assert.Equal("Java Persistence API", c.Title);
                Assert.Equal("JPA", c.Code);
                Assert.Equal(2, c.Duration);
                Assert.Collection(c.Editions,
                e => {
                    Assert.Equal(new DateTime(2018, 10, 8), e.StartDate);
                });
            });
        }

        [Fact]
        public async Task ShouldNotImportCoursesEndingBeforeStartOfRange() {

            string fakeFileContents =
$@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8/10/2018

Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 15/10/2018

Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 15/10/2018

Titel: Java Persistence API
Cursuscode: JPA
Duur: 2 dagen
Startdatum: 8/10/2018

Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 8/10/2018

";

            DateTime from = new DateTime(2018, 10, 10);
            DateTime to = new DateTime(2018, 10, 13);

            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            CourseParser sut = InitCourseImporter(fakeFileContents);

            List<Course> courses = await sut.ParseAsync(fakeMemoryStream, from, to);

            Assert.Collection(courses,
                c => {
                    Assert.Equal("C# Programmeren", c.Title);
                    Assert.Equal("CNETIN", c.Code);
                    Assert.Equal(5, c.Duration);
                    Assert.Collection(c.Editions,
                    e => {
                        Assert.Equal(new DateTime(2018, 10, 8), e.StartDate);
                    });
                });
        }
    }

}

