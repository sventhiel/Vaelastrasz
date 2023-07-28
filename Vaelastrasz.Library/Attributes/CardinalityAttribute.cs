using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Attributes
{
    public class CardinalityAttribute : ValidationAttribute
    {
        public CardinalityAttribute()
        {
            Minimum = 0;
            Maximum = int.MaxValue;
        }

        public int Maximum { get; set; }
        public int Minimum { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.GetType().GetGenericTypeDefinition() != typeof(List<>))
                return new ValidationResult("The propery is not a list.");

            var collection = (IList)value;

            if (collection.Count < Minimum)
                return new ValidationResult($"The property contains less entries than the minimum of {Minimum}.");

            if (collection.Count > Maximum)
                return new ValidationResult($"The property contains more entries than the maximum of {Maximum}.");

            return ValidationResult.Success;
        }
    }
}