using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Services {
    public class CoursesOverviewService : ICoursesOverviewService {
        private readonly ICoursesOverviewRepository coursesOverviewRepository;
        

        public CoursesOverviewService(ICoursesOverviewRepository coursesOverviewRepository) {
            this.coursesOverviewRepository = coursesOverviewRepository;
        }

        public Task<IEnumerable<CourseOverview>> GetCoursesOverview() {
            return this.coursesOverviewRepository.GetCoursesOverview();
        }
    }
}