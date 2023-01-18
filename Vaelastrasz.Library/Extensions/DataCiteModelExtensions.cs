using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        public static string Serialize(this CreateDataCiteModel model)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new[] { new StringEnumConverter() }
            };

            return JsonConvert.SerializeObject(model, Formatting.None, jsonSettings);
        }

        public static string Serialize(this ReadDataCiteModel model)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new[] { new StringEnumConverter() }
            };

            return JsonConvert.SerializeObject(model, Formatting.None, jsonSettings);
        }
    }
}