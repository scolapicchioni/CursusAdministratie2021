using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Core.CourseParsers {
    public interface ICourseParser {
        Task<List<Course>> ParseAsync(Stream stream);

        Task<List<Course>> ParseAsync(Stream stream, DateTime from, DateTime to);

        List<Course> Parse(string content);

        List<Course> Parse(string content, DateTime from, DateTime to);
    }
}