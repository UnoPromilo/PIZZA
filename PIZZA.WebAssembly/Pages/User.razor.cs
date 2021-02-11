using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PIZZA.Models.Authentication;
using PIZZA.Models.User;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class User
    {
        [Inject]
        private PopupService PopupService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        private IEmployeeService EmployeeService { get; set; } 

        private EmployeeModel Employee = new();

        [Parameter]
        public string UserID { get; set; }


        private bool IsAdmin
        {
            get
            {
                return Employee.Roles.Where(r => r.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))?.Count() > 0;
            }
            set
            {
                if(IsAdmin != value)
                {
                    if (value)
                        Employee.Roles.Add("Admin");
                    else
                        Employee.Roles.Remove("Admin");
                }
            }
        }

        private bool IsManager
        {
            get
            {
                return Employee.Roles.Where(r => r.Equals("MANAGER", StringComparison.OrdinalIgnoreCase))?.Count() > 0;
            }
            set
            {
                if (IsManager != value)
                {
                    if (value)
                        Employee.Roles.Add("Manager");
                    else
                        Employee.Roles.Remove("Manager");
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployee(UserID);
            if (UserID != Employee.ID.ToString()) NavigationManager.NavigateTo("/users");
            
            await base.OnInitializedAsync();
        }

        private void ChangePassword()
        {
            PopupChangePasswordModel popupModel = new()
            {
                Buttons = new List<PopupButton>
                {
                    new PopupButton
                    {
                        Content = "zatwierdź",
                        CloseOnClick = false
                    }
                },
                PasswordModel = new PasswordChangeModel { ID = Employee.ID },
                
            };
            popupModel.OnValidSubmit += ChangePassword;
            PopupService.AddPopupModelToStack(popupModel);
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
                    OnClick = (sender, args) => DeleteUser(Employee)
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
            NavigationManager.NavigateTo("/users");
        }

        void SaveChanges(EditContext editContext)
        {
            EmployeeService.UpdateEmployee(Employee);
        }

        void ChangePassword(object sender, EditContext editContext)
        {
            PopupService.RemoveFirstPopupModelFromStack();

        }
    }

}
