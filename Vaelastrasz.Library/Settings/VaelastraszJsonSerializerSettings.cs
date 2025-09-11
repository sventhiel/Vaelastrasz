using Newtonsoft.Json;
using Vaelastrasz.Library.Converters;

namespace Vaelastrasz.Library.Settings
{
    public static class VaelastraszJsonSerializerSettings
    {
        public static JsonSerializerSettings Settings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}