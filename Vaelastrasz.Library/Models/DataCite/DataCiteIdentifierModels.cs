using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        ARK = 1,
        arXiv = 2,
        bibcode = 3,
        DOI = 4,
        EAN13 = 5,
        EISSN = 6,
        Handle = 7,
        IGSN = 8,
        ISBN = 9,
        ISSN = 10,
        ISTC = 11,
        LISSN = 12,
        LSID = 13,
        PMID = 14,
        PURL = 15,
        UPC = 16,
        URL = 17,
        URN = 18,
        w3id = 19
    }
}