using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace PIZZA.WebAssembly.Shared.Input
{
    public partial class InputFieldSelect<TValue> : InputBase<TValue>
    {
        [Parameter]
        public Expression<Func<TValue>> For { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Description { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }


        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            if (typeof(TValue) == typeof(string))
            {
                result = (TValue)(object)value;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(TValue) == typeof(int))
            {
                int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedValue);
                result = (TValue)(object)parsedValue;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(TValue) == typeof(Guid))
            {
                Guid.TryParse(value, out var parsedValue);
                result = (TValue)(object)parsedValue;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(TValue).IsEnum)
            {
                try
                {
                    result = (TValue)Enum.Parse(typeof(TValue), value);
                    validationErrorMessage = null;

                    return true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";

                    return false;
                }
            }

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TValue)}'.");
        }
    }
}
