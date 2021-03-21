using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Server.Infrastructure.Data;
using CursusAdministratie2021.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Infrastructure.Repositories {
    public class StudentsRepositoryEF : IStudentsRepository {
        private readonly CoursesAdministrationDbContext dbContext;

        public StudentsRepositoryEF(CoursesAdministrationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<Student> CreateStudent(Student studentToAdd) {
            dbContext.Students.Add(studentToAdd);
            await dbContext.SaveChangesAsync();
            return studentToAdd;
        }
        public async Task<PrivateCitizen> CreateStudent(PrivateCitizen studentToAdd) {
            dbContext.PrivateCitizens.Add(studentToAdd);
            await dbContext.SaveChangesAsync();
            return studentToAdd;
        }
        public async Task<CompanyEmployee> CreateStudent(CompanyEmployee studentToAdd) {
            dbContext.CompanyEmployees.Add(studentToAdd);
            await dbContext.SaveChangesAsync();
            return studentToAdd;
        }

        public async Task<List<Student>> FindStudentsBy(string name="", string surname="") {
            name = name?.Trim().ToLower() ?? "";
            surname = surname?.Trim().ToLower() ?? "";
            bool namePresent = name.Length > 0;
            bool surnamePresent = surname.Length > 0;
            IQueryable<Student> query = dbContext.Students;
            if (namePresent) query = query.Where(s => s.Name.ToLower().Contains(name));
            if (surnamePresent) query = query.Where(s => s.Surname.ToLower().Contains(surname));
            return await query.OrderBy(s=>s.Surname).ThenBy(s=>s.Name).ToListAsync();
        }

        public async Task<List<CompanyEmployee>> FindCompanyEmployeesBy(string name = "", string surname = "", string companyName = "") {
            name = name?.Trim().ToLower() ?? "";
            surname = surname?.Trim().ToLower() ?? "";
            companyName = companyName?.Trim().ToLower() ?? "";
            bool namePresent = name.Length > 0;
            bool surnamePresent = surname.Length > 0;
            bool companyNamePresent = companyName.Length > 0;

            IQueryable<CompanyEmployee> query = dbContext.CompanyEmployees;
            if (namePresent) query = query.Where(s => s.Name.ToLower().Contains(name));
            if (surnamePresent) query = query.Where(s => s.Surname.ToLower().Contains(surname));
            if (companyNamePresent) query = query.Where(s => s.Company.ToLower().Contains(companyName));

            return await query.OrderBy(s => s.Surname).ThenBy(s => s.Name).ToListAsync();
        }

        public async Task<List<PrivateCitizen>> FindPrivateCitizensBy(string name = "", string surname = "") {
            name = name?.Trim().ToLower() ?? "";
            surname = surname?.Trim().ToLower() ?? "";
            bool namePresent = name.Length > 0;
            bool surnamePresent = surname.Length > 0;
            IQueryable<PrivateCitizen> query = dbContext.PrivateCitizens;
            if (namePresent) query = query.Where(s => s.Name.ToLower().Contains(name));
            if (surnamePresent) query = query.Where(s => s.Surname.ToLower().Contains(surname));
            return await query.OrderBy(s => s.Surname).ThenBy(s => s.Name).ToListAsync();
        }

        public async Task<Student> GetStudent(int id) {
            return await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<List<Student>> GetStudentsByEditionId(int editionId) {
            return dbContext.Students.Where(s => s.Editions.Any(e => e.Id == editionId)).OrderBy(s=>s.Surname).ThenBy(s=>s.Name).ToListAsync();
        }
    }
}
