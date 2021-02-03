using Microsoft.AspNetCore.Components;
using PIZZA.Models.Authentication;
using PIZZA.WebAssembly.Api.Services;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages.Authentication
{
    public partial class Login
    {
        [Inject]
        private IAuthService AuthService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private LoginModel loginModel = new LoginModel();
        private bool ShowErrors;
        private string Error = "";

        private async Task HandleLogin()
        {
            ShowErrors = false;

            var result = await AuthService.Login(loginModel);

            if (result.Successful)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Error;
                ShowErrors = true;
            }
        }
    }
}
