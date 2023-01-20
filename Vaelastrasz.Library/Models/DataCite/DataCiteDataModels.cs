using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDataModel
    {
        public string Id { get; set; }

        public DataCiteType Type { get; set; }

        public DataCiteAttributesModel Attributes { get; set; }
    }

    public enum DataCiteType
    {
        [EnumMember(Value = "dois")]
        DOIs = 1
    }
}
