using System;
using System.ComponentModel.DataAnnotations;

namespace PIZZA.Models.Validator
{
    public class CheckDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Upewnij się, że data jest z przyszłości.");
        }

    }
}
