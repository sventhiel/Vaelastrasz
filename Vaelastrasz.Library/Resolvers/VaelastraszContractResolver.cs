using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vaelastrasz.Library.Resolvers
{
    public class VaelastraszContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            // Ignore empty collections
            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                property.ShouldSerialize = instance =>
                {
                    var value = property.ValueProvider?.GetValue(instance) as System.Collections.IEnumerable;
                    return value != null && value.GetEnumerator().MoveNext();
                };
            }

            // Ignore empty strings
            if (property.PropertyType == typeof(string))
            {
                property.ShouldSerialize = instance =>
                {
                    var value = property.ValueProvider?.GetValue(instance) as string;
                    return !string.IsNullOrWhiteSpace(value);
                };
            }

            return property;
        }
    }
}