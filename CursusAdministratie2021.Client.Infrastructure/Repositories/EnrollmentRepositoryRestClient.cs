using CursusAdministratie2021.Client.Core.Interfaces;
using CursusAdministratie2021.Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Infrastructure.Repositories {
    public class EnrollmentRepositoryRestClient : IEnrollmentRepository {
        private readonly HttpClient httpClient;

        public EnrollmentRepositoryRestClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }
        public async Task EnrollStudentToEdition(int studentId, int editionId) {
            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(httpClient.BaseAddress, $"/api/enrollment/student/{studentId}/edition/{editionId}")
            };

            var response = await httpClient.SendAsync(requestMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) {
                EnrollError error = await response.Content.ReadFromJsonAsync<EnrollError>();
                throw new ValidationException(error.Message);
            }
        }
    }
}
