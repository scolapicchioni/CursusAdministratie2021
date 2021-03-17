using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesImporterController : ControllerBase {
        private readonly ICoursesImporterService coursesImporterService;

        public CoursesImporterController(ICoursesImporterService coursesImporterService) {
            this.coursesImporterService = coursesImporterService;
        }

        [HttpPost]
        public async Task<ImportCoursesReply> ImportCourses(IEnumerable<Course> coursesToImport) {
            return await coursesImporterService.ImportCourses(coursesToImport);
        }
    }
}
