using CursusAdministratie2021.Client.Core.Interfaces;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Infrastructure.Repositories {
    public class CoursesImporterRepositoryRestClient : ICoursesImporterRepository {
        private readonly HttpClient httpClient;

        public CoursesImporterRepositoryRestClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<ImportCoursesReply> ImportCourses(IEnumerable<Course> courses) {
            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(httpClient.BaseAddress, "/api/coursesimporter"),
                Content = JsonContent.Create(courses)
            };

            var response = await httpClient.SendAsync(requestMessage);
            return await response.Content.ReadFromJsonAsync<ImportCoursesReply>();
        }
    }
}
