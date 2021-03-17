using CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursusAdministratie2021.Client.Core.UnitTests.CourseParsers.CoursePropertyParsers {
    public class CourseCodeParserTests {
        [Fact]
        public void ShouldParse() {
            string text = "Cursuscode: CNETIN";
            CourseCodeParser sut = new CourseCodeParser();
            string code = sut.Parse(text);
            Assert.Equal("CNETIN", code);
        }

        [Fact]
        public void ShouldThrow() {
            string text = "Code: JPA";
            CourseCodeParser sut = new CourseCodeParser();
            Assert.Throws<ValidationException>(() => {
                string code = sut.Parse(text);
            });

        }
    }
}
