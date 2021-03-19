using CursusAdministratie2021.Client.Core.Interfaces;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Client.Infrastructure.Repositories {
    public class StudentsRepositoryRestClient : IStudentsRepository {
        private readonly HttpClient httpClient;

        public StudentsRepositoryRestClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<Student> CreateStudent(Student studentToAdd) {
            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(httpClient.BaseAddress, "/api/students"),
                Content = JsonContent.Create(studentToAdd)
            };

            var response = await httpClient.SendAsync(requestMessage);
            return await response.Content.ReadFromJsonAsync<Student>();
        }

        public async Task<List<Student>> FindStudentsBy(string name, string surname) =>          
            await httpClient.GetFromJsonAsync<List<Student>>($"/api/students/find?name={name}&surname={surname}");
        
        public async Task<List<Student>> GetStudentsByEditionId(int editionId) =>
            await httpClient.GetFromJsonAsync<List<Student>>($"/api/students/by-editionid/{editionId}");
    }
}
