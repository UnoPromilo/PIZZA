using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Models
{
    public interface IPopupModel
    {
        object Model { get; }
        IEnumerable<PopupButton> Buttons { get; }
    }
}
