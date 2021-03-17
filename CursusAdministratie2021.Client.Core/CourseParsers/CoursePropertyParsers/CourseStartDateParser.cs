using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers {
    public class CourseStartDateParser : ICoursePropertyParser<DateTime> {
        private readonly Regex regex;
        public CourseStartDateParser() {
            regex = new Regex(@"(?<=Startdatum:\s).*");
        }

        public DateTime Parse(string text) {
            Match match = regex.Match(text);
            if (!match.Success)
                throw new ValidationException("Startdatum niet gevonden");
            if (!DateTime.TryParseExact(match.Value, new string[] { "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                throw new ValidationException("Startdatum niet in correct formaat");
            return result;
        }
    }
}
