using Blazored.LocalStorage;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public class EmployeeService : AuthorizedService, IEmployeeService
    {
        public EmployeeService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage) : base(httpClientFactory, localStorage) { }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            await loadAuthTask;
            var response = await _httpClient.GetAsync("api/GetEmployees");
            var text = await response.Content.ReadAsStringAsync();
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
    }
}
