using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Core.Interfaces {
    public interface IStudentsService {
        Task<Student> CreateStudent(Student studentToAdd);
        Task<List<Student>> FindStudentsBy(string name, string surname);

        Task<Student> GetStudent(int id);
        Task<List<Student>> GetStudentsByEditionId(int editionId);
    }
}
