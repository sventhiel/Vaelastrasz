﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTitle
    {
        public DataCiteTitle()
        {
        }

        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }

        [Required]
        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        [JsonProperty("titleType")]
        [XmlElement("titleType")]
        public DataCiteTitleType TitleType { get; set; }
    }
}