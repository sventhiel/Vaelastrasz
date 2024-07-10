using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAlternateIdentifier
    {
        public DataCiteAlternateIdentifier() { }

        [JsonProperty("alternateIdentifierType")]
        [XmlElement("alternateIdentifierType")]
        public string AlternateIdentifierType { get; set; }

        [JsonProperty("alternateIdentifier")]
        [XmlElement("alternateIdentifier")]
        public string AlternateIdentifier { get; set; }

    }
}
