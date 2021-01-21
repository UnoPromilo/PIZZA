using Microsoft.AspNetCore.Components;
using PIZZA.Models.Instalation;
using PIZZA.WebAssembly.Api.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
