using Blazored.LocalStorage;
using PIZZA.Models.Authentication;
using PIZZA.Models.Results;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public class EmployeeService : AuthorizedService, IEmployeeService
    {
        public EmployeeService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage) : base(httpClientFactory, localStorage) { }

        public async Task<List<EmployeeModel>> GetEmployees(CancellationToken cancellationToken, string query)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await loadAuthTask;
            var response = await _httpClient.GetAsync($"api/GetEmployees?query={query}", cancellationToken);
            var text = await response.Content.ReadAsStringAsync(cancellationToken);
            var output = JsonSerializer.Deserialize<List<EmployeeModel>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<EmployeeModel> GetEmployee(string id)
        {
            await loadAuthTask;
            var response = await _httpClient.GetAsync($"api/Employee?id={id}");
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<EmployeeModel>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            await loadAuthTask;
            var response = await _httpClient.DeleteAsync($"api/Employee?id={id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEmployee(EmployeeModel employeeModel)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(employeeModel);
            var response = await _httpClient.PostAsync($"api/UpdateEmployee", new StringContent(content, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<RegistrationResult> CreateEmployee(RegistrationModelComplete model)
        {
            await loadAuthTask;
            var content = JsonSerializer.Serialize(model);
            var response = await _httpClient.PostAsync($"api/CreateUser", new StringContent(content, Encoding.UTF8, "application/json"));
            var text = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<RegistrationResult>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }
    }
}
