using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System;
using System.Collections.Generic;

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

        public bool PopPopupModel(IPopupModel popupModel)
        {
            if (popupModels.Peek() == popupModel)
            {
                popupModels.Pop();
                StateHasChanged();
                return true;
            }
            else return false;
        }

        public bool PopPopupModel()
        {
            return popupModels.TryPop(out IPopupModel model);
        }

        private void OnButtonClick(MouseEventArgs eventArgs, IPopupModel popupModel, PopupButton button)
        {
            button.OnClick?.Invoke(this, eventArgs);
            if(button.CloseOnClick)
                popupModels.Pop();

            StateHasChanged();
        }

        private void OnValidSubmit(object sender, EditContext context)
        {
            currentModel?.OnValidSubmit(sender, context);
        }
    }
}
