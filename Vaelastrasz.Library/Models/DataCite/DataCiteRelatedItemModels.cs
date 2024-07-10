using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedItem
    {
        public DataCiteRelatedItem()
        {
            Creators = new List<DataCiteRelatedItemCreator>();
            Titles = new List<DataCiteRelatedItemTitle>();
            Contributors = new List<DataCiteRelatedItemContributor>;
        }

        [JsonProperty("relatedItemType")]
        [XmlElement("relatedItemType")]
        public DataCiteResourceTypeGeneral RelatedItemType { get; set; }

        [JsonProperty("relationType")]
        [XmlElement("relationType")]
        public DataCiteRelationType RelationType { get; set; }

        [JsonProperty("relatedItemIdentifier")]
        [XmlElement("relatedItemIdentifier")]
        public DataCiteRelationType RelatedItemIdentifier { get; set; }

        [JsonProperty("creators")]
        [XmlElement("creators")]
        public List<DataCiteRelatedItemCreator> Creators { get; set; }

        [JsonProperty("titles")]
        [XmlElement("titles")]
        public List<DataCiteRelatedItemTitle> Titles { get; set; }

        [JsonProperty("volume")]
        [XmlElement("volume")]
        public string Volume { get; set; }

        [JsonProperty("issue")]
        [XmlElement("issue")]
        public string Issue { get; set; }

        [JsonProperty("number")]
        [XmlElement("number")]
        public string Number { get; set; }

        [JsonProperty("numberType")]
        [XmlElement("numberType")]
        public DataCiteRelatedItemNumberType NumberType { get; set; }

        [JsonProperty("firstPage")]
        [XmlElement("firstPage")]
        public string FirstPage { get; set; }

        [JsonProperty("lastPage")]
        [XmlElement("lastPage")]
        public string LastPage { get; set; }

        [JsonProperty("publisher")]
        [XmlElement("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("publicationYear")]
        [XmlElement("publicationYear")]
        public string PublicationYear { get; set; }

        [JsonProperty("edition")]
        [XmlElement("edition")]
        public string Edition { get; set; }

        [JsonProperty("contributors")]
        [XmlElement("contributors")]
        public List<DataCiteRelatedItemContributor> Contributors { get; set; }
    }
}
