using Newtonsoft.Json;

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