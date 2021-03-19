using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase {
        private readonly IEnrollmentService enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService) {
            this.enrollmentService = enrollmentService;
        }

        [HttpPost("student/{studentId}/edition/{editionId}")]
        public async Task<IActionResult> Enroll(int studentId, int editionId) {
            try {
                await enrollmentService.EnrollStudentToEdition(studentId, editionId);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new EnrollError() { Message = ex.Message });
            }
        }
    }
}
