using Microsoft.AspNetCore.Components;
using PIZZA.Models.User;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using PIZZA.WebAssembly.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class User
    {
        [Inject]
        private PopupService popupService { get; set; }

        private Employee Employee = new();

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
