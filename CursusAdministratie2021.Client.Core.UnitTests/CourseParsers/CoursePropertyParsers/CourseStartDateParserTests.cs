using CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursusAdministratie2021.Client.Core.UnitTests.CourseParsers.CoursePropertyParsers {
    public class CourseStartDateParserTests {
        [Fact]
        public void ShouldParse() {
            string text = "Startdatum: 8/10/2018";
            CourseStartDateParser sut = new CourseStartDateParser();
            DateTime startDate = sut.Parse(text);
            Assert.Equal(new DateTime(2018, 10, 8), startDate);
        }

        [Fact]
        public void ShouldThrowWhenInvalidFormat() {
            string text = "Startdatum: 8-10-2018";
            CourseStartDateParser sut = new CourseStartDateParser();
            Assert.Throws<ValidationException>(() => {
                DateTime startDate = sut.Parse(text);
            });
        }

        [Fact]
        public void ShouldThrowWhenDateNotFound() {
            string text = "Some text: 8/10/2018";
            CourseStartDateParser sut = new CourseStartDateParser();
            Assert.Throws<ValidationException>(() => {
                DateTime startDate = sut.Parse(text);
            });
        }
    }
}
