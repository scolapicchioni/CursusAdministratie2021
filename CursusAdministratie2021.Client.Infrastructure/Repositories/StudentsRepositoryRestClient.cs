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

        public async Task<PrivateCitizen> CreateStudent(PrivateCitizen studentToAdd) {
            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(httpClient.BaseAddress, "/api/students/private-citizen"),
                Content = JsonContent.Create(studentToAdd)
            };

            var response = await httpClient.SendAsync(requestMessage);
            return await response.Content.ReadFromJsonAsync<PrivateCitizen>();
        }
        public async Task<CompanyEmployee> CreateStudent(CompanyEmployee studentToAdd) {
            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(httpClient.BaseAddress, "/api/students/company-employee"),
                Content = JsonContent.Create(studentToAdd)
            };

            var response = await httpClient.SendAsync(requestMessage);
            return await response.Content.ReadFromJsonAsync<CompanyEmployee>();
        }
        public async Task<List<Student>> FindStudentsBy(string name, string surname) =>          
            await httpClient.GetFromJsonAsync<List<Student>>($"/api/students/find?name={name}&surname={surname}");
        public async Task<List<CompanyEmployee>> FindCompanyEmployeesBy(string name, string surname, string companyName) =>
            await httpClient.GetFromJsonAsync<List<CompanyEmployee>>($"/api/students/find-company-employees?name={name}&surname={surname}&companyname={companyName}");
        public async Task<List<PrivateCitizen>> FindPrivateCitizensBy(string name, string surname) =>
            await httpClient.GetFromJsonAsync<List<PrivateCitizen>>($"/api/students/find-private-citizens?name={name}&surname={surname}");

        public async Task<List<Student>> GetStudentsByEditionId(int editionId) =>
            await httpClient.GetFromJsonAsync<List<Student>>($"/api/students/by-editionid/{editionId}");
    }
}
