using CursusAdministratie2021.Client.Core.Interfaces;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Core.Services {
    public class EnrollmentService : IEnrollmentService {
        private readonly IEnrollmentRepository enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository) {
            this.enrollmentRepository = enrollmentRepository;
        }
        public async Task EnrollStudentToEdition(int studentId, int editionId) => await enrollmentRepository.EnrollStudentToEdition(studentId, editionId);        
    }
}
