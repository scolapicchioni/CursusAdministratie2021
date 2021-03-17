using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Server.Infrastructure.Data;
using CursusAdministratie2021.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Infrastructure.Repositories {
    public class CoursesImporterRepositoryEF : ICoursesImporterRepository {
        private readonly CoursesAdministrationDbContext dbContext;

        public CoursesImporterRepositoryEF(CoursesAdministrationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task AddCourse(Course courseToAdd) {
            dbContext.Update(courseToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Course> GetCourseWithEditions(Course course) => await GetCourseWithEditions(course.Code, course.Title, course.Duration);
        private async Task<Course> GetCourseWithEditions(string code, string title, int duration) => await dbContext.Courses.Include("Editions").FirstOrDefaultAsync(c => c.Code == code && c.Title == title && c.Duration == duration);
        public async Task SaveChanges() => await dbContext.SaveChangesAsync();
    }
}
