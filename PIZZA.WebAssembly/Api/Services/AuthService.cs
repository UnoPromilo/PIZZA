using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PIZZA.Models.Results;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(IHttpClientFactory httpClientFactory,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegistrationResult> Register(RegistrationModel registerModel)
        {
            var registrationAsJson = JsonSerializer.Serialize(registerModel);
            var content = new StringContent(registrationAsJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Registration", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var registrationResult = JsonSerializer.Deserialize<RegistrationResult>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return registrationResult;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var content = new StringContent(loginAsJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Login", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var loginResult = JsonSerializer.Deserialize<LoginResult>( responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
