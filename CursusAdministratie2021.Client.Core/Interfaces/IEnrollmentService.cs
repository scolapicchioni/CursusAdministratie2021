using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Core.Interfaces {
    public interface IEnrollmentService {
        Task EnrollStudentToEdition(int studentId, int editionId);
    }
}