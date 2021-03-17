using CursusAdministratie2021.Server.Infrastructure.Data;
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

        public CoursesOverviewRepositoryEF(CoursesAdministrationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<CourseOverview>> GetCoursesOverview() {
            return await(from c in dbContext.Courses
                         from e in c.Editions
                         //orderby e.StartDate, c.Title
                         select new CourseOverview { StartDate = e.StartDate, Duration = c.Duration, Title = c.Title }).ToListAsync();
        }

    }
}
