using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Server.Infrastructure.Data;
using CursusAdministratie2021.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Infrastructure.Repositories {
    public class EnrollmentRepositoryEF : IEnrollmentRepository {
        private readonly CoursesAdministrationDbContext dbContext;

        public EnrollmentRepositoryEF(CoursesAdministrationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task EnrollStudentToEdition(int studentId, int editionId) {
            Edition edition = await dbContext.Editions.FirstOrDefaultAsync(e => e.Id == editionId);
            Student student = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            edition.Students.Add(student);
            await dbContext.SaveChangesAsync();
        }
    }
}
