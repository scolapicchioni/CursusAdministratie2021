using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.CalendarHelpers {
    public class CalendarHelper : ICalendarHelper {
        private readonly CultureInfo cultureInfo;
        private readonly Calendar calendar;
        private readonly CalendarWeekRule calendarWeekRule;
        private readonly DayOfWeek firstDayOfWeek;

        public CalendarHelper(string culture = "nl-NL", string dateFormat = "dd/MM/yyyy") {
            Culture = culture;
            DateFormat = dateFormat;
            cultureInfo = new CultureInfo(Culture);
            calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule; // CalendarWeekRule.FirstFourDayWeek;
            firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            calendar = cultureInfo.Calendar;
        }
        public string Culture { get; }
        public string DateFormat { get; }

        public int GetWeekOfYearForDate(DateTime date) => calendar.GetWeekOfYear(date, calendarWeekRule, firstDayOfWeek);
        public int GetYearForDate(DateTime date) => calendar.GetYear(date);

        public int GetIso8601WeekOfYear(DateTime date) {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = calendar.GetDayOfWeek(date);
            DayOfWeek thirdDayOfWeek = firstDayOfWeek == DayOfWeek.Monday ? DayOfWeek.Wednesday : DayOfWeek.Tuesday;
            if (day >= firstDayOfWeek && day <= thirdDayOfWeek) {
                date = date.AddDays(3);
            }

            // Return the week of our adjusted day
            return GetWeekOfYearForDate(date);
        }

        public int GetLastWeekOfTheYear(int yearNumber) {
            DateTime dateToCheck = new DateTime(yearNumber, 12, 31);
            int weekNumber = GetIso8601WeekOfYear(dateToCheck);
            if (weekNumber == 1) { //it's week one of yearNumber+1, so let's go back one week
                dateToCheck = calendar.AddWeeks(dateToCheck, -1);
                weekNumber = GetIso8601WeekOfYear(dateToCheck);
            }
            return weekNumber;
        }

        public (int yearNumber, int weekNumber) NormalizeWeekYearNumber(int yearNumber, int weekNumber) {
            if (weekNumber < 1) {
                yearNumber--;
                weekNumber = GetLastWeekOfTheYear(yearNumber);
            } else if (weekNumber > GetLastWeekOfTheYear(yearNumber)) {
                yearNumber++;
                weekNumber = 1;
            }
            return (yearNumber, weekNumber);
        }

        public (DateTime begin, DateTime end) GetWeekRange(int year, int week) {
            DateTime jan1 = new DateTime(year, 1, 1);

            int firstWeek = GetWeekOfYearForDate(jan1) - 1;
            if (firstWeek > 0) firstWeek = 1;

            int daysOffset = firstDayOfWeek - jan1.DayOfWeek;

            DateTime firstWeekDayOfYear = calendar.AddWeeks(jan1.AddDays(daysOffset), firstWeek);

            var begin = calendar.AddWeeks(firstWeekDayOfYear, week - 1);
            var end = begin.AddDays(6);

            return (begin, end);
        }

        public int GetCurrentWeekOfYear() => GetWeekOfYearForDate(DateTime.Now);

        public int GetCurrentYear() => GetYearForDate(DateTime.Now);
    }
}
