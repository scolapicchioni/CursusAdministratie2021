using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers {
    public class EmptyLineParser : ICoursePropertyParser<string> {
        private readonly Regex regex;
        public EmptyLineParser() {
            regex = new Regex(@"^.{0}$");
        }
        public string Parse(string text) {
            Match match = regex.Match(text);
            if (!match.Success)
                throw new ValidationException("Lijn niet leeg");
            return match.Value;
        }
    }
}
