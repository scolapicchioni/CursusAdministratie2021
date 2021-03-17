using Xunit;
using System.ComponentModel.DataAnnotations;
using CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers;

namespace CursusAdministratie2021.Client.Core.UnitTests.CourseParsers.CoursePropertyParsers {
    public class EmptyLineParserTests {
        [Fact]
        public void ShouldParse() {
            string text = "";
            EmptyLineParser sut = new EmptyLineParser();
            string title = sut.Parse(text);
            Assert.Equal("", title);
        }

        [Fact]
        public void ShouldThrow() {
            string text = "Stuff";
            EmptyLineParser sut = new EmptyLineParser();
            Assert.Throws<ValidationException>(() => {
                string title = sut.Parse(text);
            });


        }
    }
}
