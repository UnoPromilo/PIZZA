using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PIZZA.Models.Instalation;
using System.Text.Json;
using System.Text;

namespace PIZZA.WebAssembly.Api.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public ConfigurationService(IHttpClientFactory httpClientFactory,
                                    ILocalStorageService localStorage)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            _localStorage = localStorage;
        }

        public async Task<ConfigurationStatus> GetConfigurationStateAsync()
        {
            var response = await _httpClient.GetAsync("api/instalation/GetConfigurationStatus");
            var output = JsonSerializer.Deserialize<ConfigurationStatus>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

        public async Task<HttpResponseMessage> PostConfigureServer(InstalationInfo instalationInfo)
        {
            var instalationInfoAsJson = JsonSerializer.Serialize(instalationInfo);
            return await _httpClient.PostAsync("api/instalation/ConfigureServer", new StringContent(instalationInfoAsJson, Encoding.UTF8, "application/json"));
        }
    }
}
