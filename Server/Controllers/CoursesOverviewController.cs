using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesOverviewController : ControllerBase {
        private ICoursesOverviewService coursesOverviewService;

        public CoursesOverviewController(ICoursesOverviewService coursesOverviewService) {
            this.coursesOverviewService = coursesOverviewService;
        }

        public async Task<IEnumerable<CourseOverview>> GetCoursesOverview() {
            return await coursesOverviewService.GetCoursesOverview();
        }

        [HttpGet("{yearNumber:int}/{weekNumber:int}")]
        public async Task<IEnumerable<CourseOverview>> GetCoursesPerWeek(int yearNumber, int weekNumber) {
            return await coursesOverviewService.GetCoursesPerWeek(yearNumber,weekNumber);
        }
        [HttpGet("student/{studentId:int}")]
        public async Task<IEnumerable<CourseOverview>> GetCoursesOverviewByStudentId(int studentId) {
            return await coursesOverviewService.GetCoursesOverviewByStudentId(studentId);
        }

        [HttpGet("edition/{editionId:int}")]
        public async Task<CourseOverview> GetCourseByEditionId(int editionId) {
            return await coursesOverviewService.GetCourseOverviewByEditionId(editionId);
        }
    }
}
