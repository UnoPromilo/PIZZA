using Microsoft.AspNetCore.Components;
using PIZZA.Models.Instalation;
using PIZZA.WebAssembly.Api.Services;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly
{
    public partial class App
    {
        private ConfigurationStatus ConfigurationStatus { get; set; } = null;

        [Inject]
        private IConfigurationService ConfigurationService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var configurationStatus = await ConfigurationService.GetConfigurationStateAsync();
            ConfigurationStatus = configurationStatus;
        }
    }
}
