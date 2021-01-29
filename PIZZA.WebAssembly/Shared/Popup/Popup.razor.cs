using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Shared.Popup
{
    public partial class Popup
    {
        [Parameter]
        public string PopupID { get; init; } = Guid.NewGuid().ToString();

        [Inject]
        private PopupService PopupService { get; set; }

        private Stack<IPopupModel> popupModels { get; }

        private IPopupModel currentModel => popupModels.Peek();

        public Popup()
        {
            popupModels = new();
        }

        protected override void OnInitialized()
        {
            PopupService.TryRegisterPopup(this);
        }

        public void PushPopupModel(IPopupModel popupModel)
        {
            popupModels.Push(popupModel);
            StateHasChanged();
        }

        private void OnButtonClick(MouseEventArgs eventArgs, IPopupModel popupModel, PopupButton button)
        {
            if(button.CloseOnClick)
                popupModels.Pop();

            StateHasChanged();
        }
    }
}
