using System.Collections.Generic;
using PIZZA.Models.Authentication;
namespace PIZZA.WebAssembly.Models
{
    public class PopupChangePasswordModel:IPopupModel
    {

        public PasswordChangeModel PasswordModel { get; init; }
        public IEnumerable<PopupButton> Buttons { get; set; } = new List<PopupButton>();
        public object Model => PasswordModel;


    }
}
