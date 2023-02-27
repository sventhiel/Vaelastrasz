using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteIdentifier
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("identifierType")]
        public string IdentifierType { get; set; }

        public DataCiteIdentifier()
        { }
    }
}