using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteIdentifier
    {
        public DataCiteIdentifier()
        { }

        [Required]
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [Required]
        [JsonProperty("identifierType")]
        public DataCiteIdentifierType IdentifierType { get; set; }
    }
}