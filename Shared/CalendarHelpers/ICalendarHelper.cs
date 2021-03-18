using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.CalendarHelpers {
    public interface ICalendarHelper {
        string Culture { get; }
        string DateFormat { get; }

        int GetIso8601WeekOfYear(DateTime date);
        int GetLastWeekOfTheYear(int yearNumber);
        int GetWeekOfYearForDate(DateTime date);
        (DateTime begin, DateTime end) GetWeekRange(int year, int week);
        int GetYearForDate(DateTime date);        
        (int yearNumber, int weekNumber) NormalizeWeekYearNumber(int yearNumber, int weekNumber);
        int GetCurrentWeekOfYear();
        int GetCurrentYear();
    }
}
