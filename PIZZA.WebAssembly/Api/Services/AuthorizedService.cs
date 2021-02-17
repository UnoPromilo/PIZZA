using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public class AuthorizedService
    {
        protected readonly HttpClient _httpClient;
        protected ILocalStorageService _localStorage;
        protected Task loadAuthTask;
        public AuthorizedService(IHttpClientFactory httpClientFactory,
                                 ILocalStorageService localStorage)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            loadAuthTask = LoadAuth(localStorage);
        }
        private async Task LoadAuth(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
