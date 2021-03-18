using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Core.CourseParsers {
    public class CourseParser : ICourseParser {
        private readonly ICoursePropertyParser<string> titleParser;
        private readonly ICoursePropertyParser<string> codeParser;
        private readonly ICoursePropertyParser<int> durationParser;
        private readonly ICoursePropertyParser<DateTime> startDateParser;
        private readonly ICoursePropertyParser<string> emptyLineParser;

        public CourseParser(ICoursePropertyParser<string> titleParser, ICoursePropertyParser<string> codeParser, ICoursePropertyParser<int> durationParser, ICoursePropertyParser<DateTime> startDateParser, ICoursePropertyParser<string> emptyLineParser) {
            this.titleParser = titleParser;
            this.codeParser = codeParser;
            this.durationParser = durationParser;
            this.startDateParser = startDateParser;
            this.emptyLineParser = emptyLineParser;
        }

        public async Task<List<Course>> ParseAsync(Stream stream) => await ParseAsync(stream, DateTime.MinValue, DateTime.MaxValue);

        public async Task<List<Course>> ParseAsync(Stream stream, DateTime from, DateTime to) {
            using StreamReader reader = new StreamReader(stream);
            string streamContent = await reader.ReadToEndAsync();
            return Parse(streamContent);
        }

        public List<Course> Parse(string content) => Parse(content, DateTime.MinValue, DateTime.MaxValue);

        public List<Course> Parse(string content, DateTime from, DateTime to) {
            List<Course> courses = new List<Course>();
            int lineNumber = 0;
            string[] lines = content.Split(Environment.NewLine);
            try {
                while (lineNumber < lines.Length - 1) {
                    string titleString = lines[lineNumber];
                    string title = titleParser.Parse(titleString);
                    lineNumber++;
                    string codeString = lines[lineNumber];
                    string code = codeParser.Parse(codeString);
                    lineNumber++;
                    string durationString = lines[lineNumber];
                    int duration = durationParser.Parse(durationString);
                    lineNumber++;
                    string startDateString = lines[lineNumber];
                    DateTime startDate = startDateParser.Parse(startDateString);
                    lineNumber++;
                    //if (!reader.EndOfStream) await reader.ReadLineAsync();
                    string line = lines[lineNumber];
                    string emptyLine = emptyLineParser.Parse(line);
                    lineNumber++;

                    DateTime endDate = startDate.AddDays(duration - 1);
                    if (startDate <= to && endDate >= from) {
                        Course course = new Course() { Title = title, Code = code, Duration = duration, Editions = new List<Edition>() };
                        Edition edition = new Edition() { StartDate = startDate };

                        Course existingCourse = courses.FirstOrDefault(c => c.Title == course.Title && c.Code == course.Code && c.Duration == c.Duration);
                        if (existingCourse == null) {

                            course.Editions.Add(edition);
                            courses.Add(course);
                        } else {
                            if (!existingCourse.Editions.Any(e => e.StartDate == edition.StartDate))
                                existingCourse.Editions.Add(edition);
                        }
                    }
                }
            } catch (ValidationException ex) {
                ValidationException toBeThrown = new ValidationException($"Bestand is niet in correct formaat op regel {lineNumber + 1}.", ex);
                throw toBeThrown;
            }

            return courses;
        }
    }
}