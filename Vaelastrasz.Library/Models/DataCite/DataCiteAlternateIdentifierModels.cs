using Newtonsoft.Json;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAlternateIdentifier
    {
        public DataCiteAlternateIdentifier()
        { }

        [JsonProperty("alternateIdentifier")]
        public string AlternateIdentifier { get; set; }

        [JsonProperty("alternateIdentifierType")]
        public string AlternateIdentifierType { get; set; }
    }
}