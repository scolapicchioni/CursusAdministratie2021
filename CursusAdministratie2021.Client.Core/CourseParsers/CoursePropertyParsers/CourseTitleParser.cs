using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.RegularExpressions;

namespace CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers {
    public class CourseTitleParser : ICoursePropertyParser<string> {
        private readonly Regex regex;
        public CourseTitleParser() {
            regex = new Regex(@"(?<=Titel:\s).*");
        }
        public string Parse(string text) {
            Match match = regex.Match(text);
            if (!match.Success)
                throw new ValidationException("Titel niet gevonden");
            return match.Value;
        }
    }
}