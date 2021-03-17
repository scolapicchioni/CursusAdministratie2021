using CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursusAdministratie2021.Client.Core.UnitTests.CourseParsers.CoursePropertyParsers {
    public class CourseDurationParserTests {
        [Fact]
        public void ShouldParseMultipleDays() {
            string text = "Duur: 5 dagen";
            CourseDurationParser sut = new CourseDurationParser();
            int duration = sut.Parse(text);
            Assert.Equal(5, duration);
        }

        [Fact]
        public void ShouldThrowWhenMissingDuur() {
            string text = "Duur: 5";
            CourseDurationParser sut = new CourseDurationParser();
            Assert.Throws<ValidationException>(() => {
                int duration = sut.Parse(text);
            });

        }

        [Fact]
        public void ShouldThrowWhenNotNumber() {
            string text = "Duur: abc dagen";
            CourseDurationParser sut = new CourseDurationParser();
            Assert.Throws<ValidationException>(() => {
                int duration = sut.Parse(text);
            });
        }


        [Fact]
        public void ShouldParseOneDay() {
            string text = "Duur: 1 dagen";
            CourseDurationParser sut = new CourseDurationParser();
            int duration = sut.Parse(text);
            Assert.Equal(1, duration);
        }
    }
}
