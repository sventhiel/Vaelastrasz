using System;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Attributes
{
    public class ValidEnumAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var type = value.GetType();

            if (!type.IsEnum)
                return ValidationResult.Success;

            if (!Enum.IsDefined(type, value))
            {
                return new ValidationResult($"Invalid enum value '{value}' for type '{type.Name}'.");
            }

            return ValidationResult.Success;
        }
    }
}