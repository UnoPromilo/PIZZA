using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PIZZA.Models.User;
namespace PIZZA.WebAssembly.Models
{
    public class PopupChangePasswordModel:IPopupModel
    {

        public PasswordChangeModel PasswordModel { get; init; }
        public IEnumerable<PopupButton> Buttons { get; set; } = new List<PopupButton>();
        public object Model => PasswordModel;


    }
}
