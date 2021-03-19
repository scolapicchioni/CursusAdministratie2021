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
    public class EditionsRepositoryEF : IEditionsRepository {
        private readonly CoursesAdministrationDbContext dbContext;

        public EditionsRepositoryEF(CoursesAdministrationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<Edition> GetEditionWithStudents(int editionId) {
            return await dbContext.Editions.Include(e => e.Students).FirstOrDefaultAsync(e => e.Id == editionId);
        }
    }
}
