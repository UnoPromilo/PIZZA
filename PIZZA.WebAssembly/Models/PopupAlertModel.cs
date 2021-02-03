using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace PIZZA.WebAssembly.Models
{
    public class PopupAlertModel: IPopupModel
    {
        public string Title { get; set; }
        public string ContentAsString { get; set; }
        public object Model { get; } = default;
        public MarkupString Content
        {
            get
            {
                return new MarkupString(ContentAsString);
            }
        }
        public IEnumerable<PopupButton> Buttons {get; set;} = new List<PopupButton>();
    }
}
