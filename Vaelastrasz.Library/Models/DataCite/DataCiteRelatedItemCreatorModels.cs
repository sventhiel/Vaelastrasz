using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedItemCreator
    {
        public DataCiteRelatedItemCreator() { }

        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        [JsonProperty("givenName")]
        [XmlElement("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("familyName")]
        [XmlElement("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("nameType")]
        [XmlElement("nameType")]
        public DataCiteNameType NameType { get; set; }
    }
}
