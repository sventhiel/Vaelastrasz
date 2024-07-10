using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedItemTitle
    {
        public DataCiteRelatedItemTitle() { }

        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        [JsonProperty("titleType")]
        [XmlElement("titleType")]
        public DataCiteTitleType TitleType { get; set; }
    }
}
