using CursusAdministratie2021.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Interfaces {
    public interface ICoursesOverviewRepository {
        Task<IEnumerable<CourseOverview>> GetCoursesOverview();
        Task<IEnumerable<CourseOverview>> GetCoursesPerWeek(int yearNumber, int weekNumber);

        Task<CourseOverview> GetCourseOverviewByEditionId(int editionId);
    }
}