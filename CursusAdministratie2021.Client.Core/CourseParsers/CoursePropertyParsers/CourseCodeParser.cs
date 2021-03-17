using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers {
    public class CourseCodeParser : ICoursePropertyParser<string> {
        private readonly Regex regex;
        public CourseCodeParser() {
            regex = new Regex(@"(?<=Cursuscode:\s).*");
        }
        public string Parse(string text) {
            Match match = regex.Match(text);
            if (!match.Success)
                throw new ValidationException("Cursuscode niet gevonden");
            return match.Value;
        }
    }
}
