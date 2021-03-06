using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Services {
    public class CoursesOverviewService : ICoursesOverviewService {
        private readonly ICoursesOverviewRepository coursesOverviewRepository;

        public CoursesOverviewService(ICoursesOverviewRepository coursesOverviewRepository) => this.coursesOverviewRepository = coursesOverviewRepository;

        public Task<CourseOverview> GetCourseOverviewByEditionId(int editionId) => coursesOverviewRepository.GetCourseOverviewByEditionId(editionId);

        public Task<IEnumerable<CourseOverview>> GetCoursesOverview() => coursesOverviewRepository.GetCoursesOverview();

        public Task<IEnumerable<CourseOverview>> GetCoursesOverviewByStudentId(int studentId) => coursesOverviewRepository.GetCoursesOverviewByStudentId(studentId);

        public Task<IEnumerable<CourseOverview>> GetCoursesPerWeek(int yearNumber, int weekNumber) => coursesOverviewRepository.GetCoursesPerWeek(yearNumber, weekNumber);
    }
}