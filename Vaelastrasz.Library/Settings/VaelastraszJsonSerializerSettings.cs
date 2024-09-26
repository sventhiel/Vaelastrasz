using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
