using Xunit;
using System.ComponentModel.DataAnnotations;
using CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers;

namespace CursusAdministratie2021.Client.Core.UnitTests.CourseParsers.CoursePropertyParsers {
    public class CourseTitleParserTests {
        [Fact]
        public void ShouldParse() {
            string text = "Titel: C# Programmeren";
            CourseTitleParser sut = new CourseTitleParser();
            string title = sut.Parse(text);
            Assert.Equal("C# Programmeren", title);
        }

        [Fact]
        public void ShouldThrow() {
            string text = "Tittirittititel: C# Programmeren";
            CourseTitleParser sut = new CourseTitleParser();
            Assert.Throws<ValidationException>(() => {
                string title = sut.Parse(text);
            });


        }
    }
}
