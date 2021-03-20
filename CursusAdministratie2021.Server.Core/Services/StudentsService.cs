using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Core.Services {
    public class StudentsService : IStudentsService {
        private readonly IStudentsRepository studentsRepository;

        public StudentsService(IStudentsRepository studentsRepository) {
            this.studentsRepository = studentsRepository;
        }

        public async Task<Student> CreateStudent(Student studentToAdd) => await studentsRepository.CreateStudent(studentToAdd);
        public async Task<PrivateCitizen> CreateStudent(PrivateCitizen studentToAdd) => await studentsRepository.CreateStudent(studentToAdd);
        public async Task<CompanyEmployee> CreateStudent(CompanyEmployee studentToAdd) => await studentsRepository.CreateStudent(studentToAdd);

        public async Task<List<Student>> FindStudentsBy(string name, string surname) => await studentsRepository.FindStudentsBy(name, surname);

        public Task<Student> GetStudent(int id) => studentsRepository.GetStudent(id);

        public async Task<List<Student>> GetStudentsByEditionId(int editionId) => await studentsRepository.GetStudentsByEditionId(editionId);
    }
}
