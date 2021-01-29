using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Models
{
    public class PopupButton
    {
        public string Content { get; set; }
        public EventHandler<MouseEventArgs> OnClick { get; set; }
        public bool Disabled { get; set; } = false;
        public bool CloseOnClick { get; set; } = true;

    }
}
