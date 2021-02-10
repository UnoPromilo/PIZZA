using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using PIZZA.Models.Authentication;
namespace PIZZA.WebAssembly.Models
{
    public class PopupChangePasswordModel:IPopupModel
    {

        public PasswordChangeModel PasswordModel { get; set; }
        public IEnumerable<PopupButton> Buttons { get; set; } = new List<PopupButton>();
        public object Model { get => PasswordModel; set => PasswordModel = (PasswordChangeModel)value; } 
        public EventHandler<EditContext> OnValidSubmit { get; set; }
    }
}
