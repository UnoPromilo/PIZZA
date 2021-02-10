using Microsoft.AspNetCore.Components;
using PIZZA.Models.Authentication;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class NewUser
    {
        private RegistrationModelComplete RegistrationModel = new();

        [Inject]
        private IEmployeeService EmployeeService { get; set; }

        [Inject]
        private PopupService PopupService { get; set; }


        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private async Task Register()
        {
            var response = await EmployeeService.CreateEmployee(RegistrationModel);
            if (response.Successful)
            {
                System.Console.WriteLine($"User created, ID: {response.ID}");
                NavigationManager.NavigateTo($"/user/{response.ID}");
            }
            else
            {
                string responseString = "";
                foreach (var item in response.Errors)
                    responseString += $"<li>{item}</li>";
                PopupAlertModel model = new()
                {
                    Title = "BŁĄD",
                    ContentAsString = "<p>Wystąpiły następujące błędy:</p>" +
                                      $"<ul> {responseString}</ul>",
                    Buttons = new List<PopupButton>
                    {
                        new PopupButton
                        {
                            Content = "OK",
                        },
                    }
                };
                PopupService.AddPopupModelToStack(model);

            }
        }

    }
}
  

 