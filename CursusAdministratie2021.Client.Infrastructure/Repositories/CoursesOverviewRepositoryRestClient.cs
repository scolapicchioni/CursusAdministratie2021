using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Infrastructure.Repositories {
    public class CoursesOverviewRepositoryRestClient : ICoursesOverviewRepository {
        private readonly HttpClient httpClient;

        public CoursesOverviewRepositoryRestClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<CourseOverview> GetCourseOverviewByEditionId(int editionId) {
            return await httpClient.GetFromJsonAsync<CourseOverview>($"/api/coursesoverview/edition/{editionId}");
        }
        public async Task<IEnumerable<CourseOverview>> GetCoursesOverviewByStudentId(int studentId) {
            return await httpClient.GetFromJsonAsync<List<CourseOverview>>($"/api/coursesoverview/student/{studentId}");
        }

        public async Task<IEnumerable<CourseOverview>> GetCoursesOverview() {
            return await httpClient.GetFromJsonAsync<List<CourseOverview>>($"/api/coursesoverview");
        }

        public async Task<IEnumerable<CourseOverview>> GetCoursesPerWeek(int yearNumber, int weekNumber) {
            return await httpClient.GetFromJsonAsync<List<CourseOverview>>($"/api/coursesoverview/{yearNumber}/{weekNumber}");
        }
    }
}
