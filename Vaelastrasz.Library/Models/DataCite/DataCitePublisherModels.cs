using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCitePublisher
    {
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        [JsonProperty("publisherIdentifier")]
        [XmlElement("publisherIdentifier")]
        public string PublisherIdentifier { get; set; }

        [JsonProperty("publisherIdentifierScheme")]
        [XmlElement("publisherIdentifierScheme")]
        public string PublisherIdentifierScheme { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }

        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }

        public DataCitePublisher()
        {

        }
    }
}
