using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;

namespace PIZZA.WebAssembly.Models
{
    public interface IPopupModel
    {
        object Model { get; }
        IEnumerable<PopupButton> Buttons { get; }

        EventHandler<EditContext> OnValidSubmit { get; set; }
    }
}
