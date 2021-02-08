using Microsoft.AspNetCore.Components;
using PIZZA.Models.Authentication;
using PIZZA.Models.User;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class User
    {
        [Inject]
        private PopupService popupService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        private IEmployeeService employeeService { get; set; } 

        private EmployeeModel Employee = new();

        [Parameter]
        public string UserID { get; set; }

        protected override async Task OnInitializedAsync()
        {

            Employee = await employeeService.GetEmployee(UserID);
            if (Employee == default) NavigationManager.NavigateTo("/");
            await base.OnInitializedAsync();
        }

        private void ChangePassword()
        {
            PopupChangePasswordModel popupModel = new()
            {
                Buttons = new List<PopupButton>
                {
                    new PopupButton { Content = "zatwierdź" }
                },
                PasswordModel = new PasswordChangeModel { ID = 1 }
            };
            popupService.AddPopupModelToStack(popupModel);
            
        }
    }

}
