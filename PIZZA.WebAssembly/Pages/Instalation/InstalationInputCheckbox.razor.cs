﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq.Expressions;

namespace PIZZA.WebAssembly.Pages.Instalation
{
    public partial class InstalationInputCheckbox : InputBase<bool>
    {
        [Parameter]
        public Expression<Func<bool>> For { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Description { get; set; }



        protected override bool TryParseValueFromString(string value, out bool result, out string validationErrorMessage)
        {
            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(bool)}'.");
        }
    }
}
