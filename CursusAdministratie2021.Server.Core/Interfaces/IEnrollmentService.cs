using CursusAdministratie2021.Shared.Models;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Core.Interfaces {
    public interface IEnrollmentService {
        Task EnrollStudentToEdition(int studentId, int editionId);
    }
}