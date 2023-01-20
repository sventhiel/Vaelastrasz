using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("identifierType")]
        public DataCiteIdentifierType IdentifierType { get; set; }

        [JsonConstructor]
        public DataCiteIdentifier()
        { }

        public DataCiteIdentifier(string identifier, DataCiteIdentifierType identifierType)
        {
            Identifier = identifier;
            IdentifierType = identifierType;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteIdentifierType
    {
        [EnumMember(Value = "DOI")]
        DOI = 1
    }
}