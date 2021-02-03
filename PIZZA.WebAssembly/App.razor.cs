using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PIZZA.Models.Instalation;
using PIZZA.WebAssembly.Api.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly
{
    public partial class App
    {
        private ConfigurationStatus ConfigurationStatus { get; set; } = null;
      
        [Inject]
        private IConfigurationService ConfigurationService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var configurationStatus = await ConfigurationService.GetConfigurationStateAsync();
            ConfigurationStatus = configurationStatus;
        }   

        protected string NavigateToLogin()
        {
            NavigationManager.NavigateTo("authentication/login");
            return "";
        }
    }
}
