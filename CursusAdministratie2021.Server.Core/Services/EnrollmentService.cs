using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Core.Services {
    public class EnrollmentService : IEnrollmentService {
        private readonly IStudentsService studentsService;
        private readonly IEditionsService editionsService;
        private readonly IEnrollmentRepository enrollmentRepository;

        public EnrollmentService(IStudentsService studentsService, IEditionsService editionsService, IEnrollmentRepository enrollmentRepository) {
            this.studentsService = studentsService;
            this.editionsService = editionsService;
            this.enrollmentRepository = enrollmentRepository;
        }
        public async Task EnrollStudentToEdition(int studentId, int editionId) {
            Edition existingEdition = await editionsService.GetEditionWithStudents(editionId);
            if (existingEdition == null) {
                throw new ValidationException("Cursusinstantie niet gevonden");
            }
            int studentCount = existingEdition.Students.Count;
            if (studentCount >= 12) {
                throw new ValidationException("Er zijn al 12 inschrijvingen voor deze cursusinstantie");
            }
            Student student = existingEdition.Students.FirstOrDefault(s => s.Id == studentId);
            if (student is not null) {
                throw new ValidationException("Student al ingeschreven.");
            }

            student = await studentsService.GetStudent(studentId);
            if (student is null) {
                throw new ValidationException("Student niet gevonden.");
            }
            await enrollmentRepository.EnrollStudentToEdition(studentId, editionId);
        }
    }
}
