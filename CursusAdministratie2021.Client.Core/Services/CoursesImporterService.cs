using CursusAdministratie2021.Client.Core.Interfaces;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Core.Services {
    public class CoursesImporterService : ICoursesImporterService {
        private readonly ICoursesImporterRepository coursesImporterRepository;

        public CoursesImporterService(ICoursesImporterRepository coursesImporterRepository) {
            this.coursesImporterRepository = coursesImporterRepository;
        }
        public Task<ImportCoursesReply> ImportCourses(IEnumerable<Course> courses) => coursesImporterRepository.ImportCourses(courses);
    }
}
