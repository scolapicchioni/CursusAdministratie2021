using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CursusAdministratie2021.Shared.CalendarHelpers;

namespace CursusAdministratie2021.Shared.UnitTests.CalendarHelpers {
    public class CalendarHelpersTests {
        CalendarHelper calendarHelper = new CalendarHelper();
        public CalendarHelpersTests() {
            calendarHelper = new CalendarHelper();
        }
        [Theory]
        [InlineData(2020, 01, 2019, 12, 30, 2020, 01, 05)]
        [InlineData(2020, 02, 2020, 01, 06, 2020, 01, 12)]
        [InlineData(2020, 53, 2020, 12, 28, 2021, 01, 03)]
        [InlineData(2021, 01, 2021, 01, 04, 2021, 01, 10)]
        public void GetWeekRangeShouldReturnCorrectWeekRange(int year, int week, int expectedBeginYear, int expectedBeginMonth, int expectedBeginDay, int expectedEndYear, int expectedEndMonth, int expectedEndDay) {
            (DateTime begin, DateTime end) = calendarHelper.GetWeekRange(year, week);
            Assert.Equal(new DateTime(expectedBeginYear, expectedBeginMonth, expectedBeginDay), begin);
            Assert.Equal(new DateTime(expectedEndYear, expectedEndMonth, expectedEndDay), end);
        }

        [Theory]
        [InlineData(2020, 01, 2020, 01)]
        [InlineData(2020, -1, 2019, 52)]
        [InlineData(2020, 53, 2020, 53)]
        [InlineData(2020, 54, 2021, 1)]
        public void NormalizeWeekYearNumberShouldNormalize(int originalYearNumber, int originalWeekNumber, int expectedYearNumber, int expectedWeekNumber) {
            (int actualYearNumber, int actualWeekNumber) = calendarHelper.NormalizeWeekYearNumber(originalYearNumber, originalWeekNumber);
            Assert.Equal(expectedYearNumber, actualYearNumber);
            Assert.Equal(expectedWeekNumber, actualWeekNumber);
        }

        [Theory]
        [InlineData(2019, 12, 31, 01)]
        [InlineData(2019, 12, 29, 52)]
        [InlineData(2020, 12, 31, 53)]
        public void GetIso8601WeekOfYearShouldReturnCorrectISOWeek(int year, int month, int day, int expectedWeekNumber) {
            int actualWeekNumber = calendarHelper.GetIso8601WeekOfYear(new DateTime(year, month, day));
            Assert.Equal(expectedWeekNumber, actualWeekNumber);
        }

        [Theory]
        [InlineData(2019, 12, 31, 01)]
        [InlineData(2019, 12, 29, 1)]
        [InlineData(2020, 12, 31, 53)]
        public void GetIso8601WeekOfYearShouldReturnCorrectISOWeekForAmerica(int year, int month, int day, int expectedWeekNumber) {
            calendarHelper = new CalendarHelper(culture: "en-US");
            int actualWeekNumber = calendarHelper.GetIso8601WeekOfYear(new DateTime(year, month, day));
            Assert.Equal(expectedWeekNumber, actualWeekNumber);
        }

        [Theory]
        [InlineData(2019, 52)]
        [InlineData(2020, 53)]
        public void GetLastWeekOfTheYearShouldReturnCorrectValues(int year, int expectedWeekNumber) {
            int actualWeekNumber = calendarHelper.GetLastWeekOfTheYear(year);
            Assert.Equal(expectedWeekNumber, actualWeekNumber);
        }
    }
}
