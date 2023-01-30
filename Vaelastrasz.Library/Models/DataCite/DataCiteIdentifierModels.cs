using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteIdentifierModel
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("identifierType")]
        public DataCiteIdentifierType IdentifierType { get; set; }

        [JsonConstructor]
        public DataCiteIdentifierModel() { }

        public DataCiteIdentifierModel(string identifier, DataCiteIdentifierType identifierType)
        {
            Identifier = identifier;
            IdentifierType = identifierType;
        }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteIdentifierType
    {
        [JsonPropertyName("DOI")]
        DOI = 1
    }
}