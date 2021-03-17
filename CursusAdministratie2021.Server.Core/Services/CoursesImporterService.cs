using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Core.Services {
    public class CoursesImporterService : ICoursesImporterService {
        private readonly ICoursesImporterRepository coursesImporterRepository;
        public CoursesImporterService(ICoursesImporterRepository coursesImporterRepository) {
            this.coursesImporterRepository = coursesImporterRepository;
        }

        public async Task<ImportCoursesReply> ImportCourses(IEnumerable<Course> courses) {
            int coursesAdded = 0;
            int editionsAdded = 0;
            foreach (var courseToAdd in courses) {
                Course existing = await coursesImporterRepository.GetCourseWithEditions(courseToAdd);
                if (existing is null) {
                    await coursesImporterRepository.AddCourse(courseToAdd);
                    coursesAdded++;
                    editionsAdded += courseToAdd.Editions.Count();
                } else {
                    foreach (Edition courseToAdd_Edition in courseToAdd.Editions) {
                        if (existing.Editions.FirstOrDefault(e => e.StartDate == courseToAdd_Edition.StartDate) is null) {
                            existing.Editions.Add(courseToAdd_Edition);
                            editionsAdded++;
                        }
                    }
                }
            }

            await coursesImporterRepository.SaveChanges();
            return new ImportCoursesReply() { CoursesAdded = coursesAdded, EditionsAdded = editionsAdded };
        }

    }
}
