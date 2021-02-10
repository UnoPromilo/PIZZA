using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Shared.Popup;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PIZZA.WebAssembly.Service
{
    public class PopupService
    {
        private IDictionary<string, Popup> popups = new Dictionary<string, Popup>();
        
        private string _defPopupKey;

        public PopupService(){}

        public PopupService(string defaultPopupKey)
        {
            _defPopupKey = defaultPopupKey;
        }
        private Popup DefaultPopup
        {   get
            {
                if (popups.ContainsKey(_defPopupKey))
                {
                    return popups[_defPopupKey];
                }
                else if(popups.Count > 0)
                {
                    return popups.Values.ElementAt(0);
                }
                else
                {
                    throw new Exception("No popup controlers has been registered.");
                }
            }
            set
            {
                if (popups.Values.Contains(value))
                {
                    _defPopupKey = value.PopupID;
                }
            }
        }

        public void RegisterPopup(Popup popup)
        {
            if (popups.ContainsKey(popup.PopupID))
            {
                throw new Exception($"Popup ID {popup.PopupID} has been alredy registered.");
            }
            else
            {
                popups.Add(popup.PopupID, popup);
            }
        }

        public bool TryRegisterPopup(Popup popup)
        {
            if (popups.ContainsKey(popup.PopupID))
            {
                return false;
            }
            else
            {
                popups.Add(popup.PopupID, popup);
                return true;
            }
        }

        public bool AddPopupModelToStack(IPopupModel model,string popupID = null)
        {
            if(popupID == null)
            {
                DefaultPopup.PushPopupModel(model);
                return true;
            }
            else
            {
                if(popups.TryGetValue(popupID, out Popup popup))
                {
                    popup.PushPopupModel(model);
                    return true;
                }
            }
            return false;
        }

        public bool RemovePopupModelFromStack(IPopupModel model, string popupID = null)
        {
            if (popupID == null)
            {
                return DefaultPopup.PopPopupModel(model);
            }
            else
            {
                if (popups.TryGetValue(popupID, out Popup popup))
                {
                    return popup.PopPopupModel(model);
                }
            }
            return false;
        }
        public bool RemoveFirstPopupModelFromStack(string popupID = null)
        {
            if (popupID == null)
            {
                return DefaultPopup.PopPopupModel();
            }
            else
            {
                if (popups.TryGetValue(popupID, out Popup popup))
                {
                    return popup.PopPopupModel();
                }
            }
            return false;
        }
    }
}
