using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Extensions
{
    public static class DataCiteModelExtensions
    {
        public static bool Validate(this CreateDataCiteModel model, out List<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            var validator = new DataAnnotationsValidator.DataAnnotationsValidator();

            return validator.TryValidateObjectRecursive(model, results);
        }
    }
}