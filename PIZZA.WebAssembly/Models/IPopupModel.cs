using System.Collections.Generic;

namespace PIZZA.WebAssembly.Models
{
    public interface IPopupModel
    {
        object Model { get; }
        IEnumerable<PopupButton> Buttons { get; }
    }
}
