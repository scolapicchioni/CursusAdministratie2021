using CursusAdministratie2021.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Interfaces {
    public interface ICoursesOverviewService {
        Task<IEnumerable<CourseOverview>> GetCoursesOverview();
        Task<IEnumerable<CourseOverview>> GetCoursesPerWeek(int yearNumber, int weekNumber);
        Task<CourseOverview> GetCourseOverviewByEditionId(int editionId);
    }
}
