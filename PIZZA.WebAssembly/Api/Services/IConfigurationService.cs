using PIZZA.Models.Instalation;
using System.Net.Http;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public interface IConfigurationService
    {
        Task<ConfigurationStatus> GetConfigurationStateAsync();
        Task<HttpResponseMessage> PostConfigureServer(InstalationInfo instalationInfo);
    }
}
