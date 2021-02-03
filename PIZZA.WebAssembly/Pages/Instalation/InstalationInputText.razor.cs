using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq.Expressions;

namespace PIZZA.WebAssembly.Pages.Instalation
{
    public partial class InstalationInputText : InputBase<string>
    {
        [Parameter]
        public Expression<Func<string>> For { get; set; }

        [Parameter]
        public string Name { get; set; }
        
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Description { get; set; }

        [Parameter]
        public string Type { get; set; } = "text";

        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}
