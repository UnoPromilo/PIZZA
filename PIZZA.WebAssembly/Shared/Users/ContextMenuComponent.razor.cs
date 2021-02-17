using BlazorContextMenu;
using Microsoft.AspNetCore.Components;
using PIZZA.Models.User;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System.Collections.Generic;

namespace PIZZA.WebAssembly.Shared.Users
{
    public partial class ContextMenuComponent
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] PopupService PopupService { get; set; }

        [Inject] IEmployeeService EmployeeService { get; set; }

        private EmployeeModel currentEmployee;


        void Navigate()
        {
            NavigationManager.NavigateTo($"user/{currentEmployee.ID}");
        }

        void Delete()
        {
            PopupAlertModel model = new()
            {
                Title = "USUWANIE",
                ContentAsString = "<p>Czy na pewno chcesz usunąć?</br> Uwaga, tej operacji nie można cofnąć!</p>",
                Buttons = new List<PopupButton>
            {
                new PopupButton
                {
                    Content = "Tak",
                    OnClick = (sender, args) => DeleteUser(currentEmployee)
                },

                new PopupButton
                {
                    Content = "Nie"
                }
            }
            };

            PopupService.AddPopupModelToStack(model);
        }

        void DeleteUser(EmployeeModel employee)
        {
            EmployeeService.DeleteEmployee(employee.ID.ToString());
        }

        void MenuOnAppearingHandler(MenuAppearingEventArgs e)
        {
            currentEmployee = e.Data as EmployeeModel;
            if (currentEmployee is null) e.PreventShow = true;
        }
    }
}
