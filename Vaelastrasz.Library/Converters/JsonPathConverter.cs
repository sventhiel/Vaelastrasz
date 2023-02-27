using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Vaelastrasz.Library.Converters
{
    /*
     * Links
     * - https://stackoverflow.com/questions/52619432/json-net-deserialize-object-nested-data
     * - https://stackoverflow.com/questions/33088462/can-i-specify-a-path-in-an-attribute-to-map-a-property-in-my-class-to-a-child-pr
     *
     */
    public class JsonPathConverter : JsonConverter
    {
        /// <inheritdoc />
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            object targetObj = Activator.CreateInstance(objectType);
            foreach (PropertyInfo prop in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                JsonPropertyAttribute att = prop.GetCustomAttributes(true)
                                                .OfType<JsonPropertyAttribute>()
                                                .FirstOrDefault();
                string jsonPath = att != null ? att.PropertyName : prop.Name;
                if (serializer.ContractResolver is DefaultContractResolver)
                {
                    var resolver = (DefaultContractResolver)serializer.ContractResolver;
                    jsonPath = resolver.GetResolvedPropertyName(jsonPath);
                }
                if (!Regex.IsMatch(jsonPath, @"^[a-zA-Z0-9_.-]+$"))
                {
                    throw new InvalidOperationException("JProperties of JsonPathConverter can have only letters, numbers, underscores, hiffens and dots but name was '" +jsonPath + "'."); // Array operations not permitted
                }
                JToken token = jo.SelectToken(jsonPath);
                if (token != null && token.Type != JTokenType.Null)
                {
                    object value = token.ToObject(prop.PropertyType, serializer);
                    prop.SetValue(targetObj, value, null);
                }
            }
            return targetObj;
        }
        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called when [JsonConverter] attribute is used
            return objectType.GetCustomAttributes(true).OfType<JsonPathConverter>().Any();
        }
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var properties = value.GetType().GetRuntimeProperties().Where(p => p.CanRead && p.CanWrite);
            JObject main = new JObject();
            foreach (PropertyInfo prop in properties)
            {
                // probably not necessary
                //if (prop.GetValue(value) == null)
                //    continue;

                JsonPropertyAttribute att = prop.GetCustomAttributes(true)
                    .OfType<JsonPropertyAttribute>()
                    .FirstOrDefault();
                string jsonPath = (att != null ? att.PropertyName : prop.Name);
                var nesting = jsonPath.Split(new[] { '.' });
                JObject lastLevel = main;
                for (int i = 0; i < nesting.Length; i++)
                {
                    if (i == nesting.Length - 1)
                    {
                        lastLevel[nesting[i]] = new JValue(prop.GetValue(value));
                    }
                    else
                    {
                        if (lastLevel[nesting[i]] == null)
                        {
                            lastLevel[nesting[i]] = new JObject();
                        }
                        lastLevel = (JObject)lastLevel[nesting[i]];
                    }
                }
            }
            serializer.Serialize(writer, main);
        }
    }
}