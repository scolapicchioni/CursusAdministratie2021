using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Core.Interfaces {
    public interface ICoursesImporterRepository {
        Task<ImportCoursesReply> ImportCourses(IEnumerable<Course> courses);
    }
}
