using CursusAdministratie2021.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Core.Interfaces {
    public interface IStudentsRepository {
        Task<Student> CreateStudent(Student studentToAdd);
        Task<PrivateCitizen> CreateStudent(PrivateCitizen studentToAdd);
        Task<CompanyEmployee> CreateStudent(CompanyEmployee studentToAdd);
        Task<List<Student>> FindStudentsBy(string name, string surname);
        Task<List<CompanyEmployee>> FindCompanyEmployeesBy(string name, string surname, string companyName);
        Task<List<PrivateCitizen>> FindPrivateCitizensBy(string name, string surname);
        Task<List<Student>> GetStudentsByEditionId(int editionId);
    }
}