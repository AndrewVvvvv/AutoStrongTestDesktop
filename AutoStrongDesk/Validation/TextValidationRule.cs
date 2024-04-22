using System.Globalization;
using System.Windows.Controls;

namespace AutoStrongDesk.Validation
{
    internal class TextValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? valueToValidate = value as string;

            if (!string.IsNullOrEmpty(valueToValidate) && valueToValidate.Length > 100)
            {
                return new ValidationResult(false, "Text length should be less than 100 symbols");
            }

            return ValidationResult.ValidResult;
        }
    }
}
