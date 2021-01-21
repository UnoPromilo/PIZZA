using PIZZA.Models.Instalation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Configuration
{
    public interface IConfigurationService
    {
        Task<ConfigurationStatus> GetConfigurationStateAsync();
        Task<HttpResponseMessage> PostConfigureServer(InstalationInfo instalationInfo);
    }
}
