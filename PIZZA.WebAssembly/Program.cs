using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PIZZA.WebAssembly.Api;
using PIZZA.WebAssembly.Api.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using PIZZA.WebAssembly.Extension;
using System.Net.Http.Headers;

namespace PIZZA.WebAssembly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient("api",
               (HttpClient client) =>
               {
                   client.BaseAddress = new Uri(builder.Configuration["Server"]);
               });
            ConfigureServices(builder.Services);
            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage()
                    .AddTransient<IConfigurationService, ConfigurationService>()
                    .AddTransient<IEmployeeService, EmployeeService>()
                    .AddTransient<ITaskService, TaskService>()
                    .AddAuthorizationCore()
                    .AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
                    .AddScoped<IAuthService, AuthService>()
                    .AddBlazorContextMenu(options =>
                    {
                        options.ConfigureTemplate(defaultTemplate =>
                        {
                            defaultTemplate.MenuItemCssClass = "context__button";
                            defaultTemplate.MenuShownCssClass = "context";
                        });
                    })
                    .AddPopupService("default")
                    .AddAuthorizationCore()
                    .AddOptions();
        }
    }
}
