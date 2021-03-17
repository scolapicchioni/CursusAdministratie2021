using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers {
    public class CourseDurationParser : ICoursePropertyParser<int> {
        private readonly Regex regex;
        public CourseDurationParser() {
            regex = new Regex(@"(?<=Duur:\s).*(?=dagen)");
        }

        public int Parse(string text) {
            Match match = regex.Match(text);
            if (!match.Success)
                throw new ValidationException("Duur niet gevonden");
            if (!int.TryParse(match.Value, out int result))
                throw new ValidationException("Duur niet gevonden");
            return result;
        }
    }
}
