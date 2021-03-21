using CursusAdministratie2021.Server.Infrastructure.Data;
using CursusAdministratie2021.Shared.CalendarHelpers;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Infrastructure.Repositories {
    public class CoursesOverviewRepositoryEF : ICoursesOverviewRepository {
        private readonly CoursesAdministrationDbContext dbContext;
        private readonly ICalendarHelper calendarHelper;

        public CoursesOverviewRepositoryEF(CoursesAdministrationDbContext dbContext, ICalendarHelper calendarHelper) {
            this.dbContext = dbContext;
            this.calendarHelper = calendarHelper;
        }

        public async Task<CourseOverview> GetCourseOverviewByEditionId(int editionId) {
            return await(from c in dbContext.Courses
                         from e in c.Editions
                         where e.Id == editionId
                         select new CourseOverview { StartDate = e.StartDate, Duration = c.Duration, Title = c.Title, EditionId = e.Id, EnrollmentsCount = e.Students.Count }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CourseOverview>> GetCoursesOverview() {
            return await(from c in dbContext.Courses
                         from e in c.Editions
                         orderby e.StartDate, c.Title
                         select new CourseOverview { StartDate = e.StartDate, Duration = c.Duration, Title = c.Title, EditionId = e.Id, EnrollmentsCount = e.Students.Count }).ToListAsync();
        }

        public async Task<IEnumerable<CourseOverview>> GetCoursesPerWeek(int year, int week) {
            (DateTime begin, DateTime end) = calendarHelper.GetWeekRange(year, week);

            return await (from c in dbContext.Courses
                          from e in c.Editions
                          where e.StartDate >= begin && e.StartDate <= end
                          orderby e.StartDate, c.Title
                          select new CourseOverview { StartDate = e.StartDate, Duration = c.Duration, Title = c.Title, EditionId = e.Id, EnrollmentsCount = e.Students.Count }).ToListAsync();
        }
        public async Task<IEnumerable<CourseOverview>> GetCoursesOverviewByStudentId(int studentId) {
            return await (from c in dbContext.Courses
                          from e in c.Editions
                          where e.Students.Any(s=>s.Id==studentId)
                          orderby e.StartDate, c.Title
                          select new CourseOverview { StartDate = e.StartDate, Duration = c.Duration, Title = c.Title, EditionId = e.Id, EnrollmentsCount = e.Students.Count }).ToListAsync();
        }
    }
}
