using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase {
        private readonly IStudentsService studentsService;

        public StudentsController(IStudentsService studentsService) {
            this.studentsService = studentsService;
        }

        [HttpGet("find")]
        public Task<List<Student>> FindStudentsBy([FromQuery]string name="", [FromQuery]string surname="") {
            return studentsService.FindStudentsBy(name, surname);
        }

        [HttpGet("by-editionid/{editionId:int}")]
        public Task<List<Student>> GetStudentsByEditionId(int editionId) {
            return studentsService.GetStudentsByEditionId(editionId);
        }

        [HttpPost()]
        public Task<Student> AddStudent(Student student) {
            return studentsService.CreateStudent(student);
        }
    }
}
