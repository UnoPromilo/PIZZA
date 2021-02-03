using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Shared.Input
{
    public partial class InputFieldDate : InputBase<DateTime>
    {
        private const string DateFormat = "yyyy-MM-dd";

        [Parameter]
        public Expression<Func<DateTime>> For { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter] 
        public string ParsingErrorMessage { get; set; } = "Pole {0} musi być datą.";


        protected override string FormatValueAsString(DateTime value)
        {
            return BindConverter.FormatValue(value, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out DateTime result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            // Unwrap nullable types. We don't have to deal with receiving empty values for nullable
            // types here, because the underlying InputBase already covers that.
            var targetType = Nullable.GetUnderlyingType(typeof(DateTime)) ?? typeof(DateTime);

            bool success = TryParseDateTime(value, out result);

            if (success)
            {
                validationErrorMessage = null;
                return true;
            }
            else
            {
                validationErrorMessage = string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayName ?? FieldIdentifier.FieldName);
                return false;
            }
        }

        private static bool TryParseDateTime(string? value, [MaybeNullWhen(false)] out DateTime result)
        {
            var success = BindConverter.TryConvertToDateTime(value, CultureInfo.InvariantCulture, DateFormat, out var parsedValue);
            if (success)
            {
                result = parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        
    }
}
