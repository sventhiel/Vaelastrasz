using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Vaelastrasz.Library.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DOIStateType
    {
        [EnumMember(Value = "findable")]
        Findable = 1,

        [EnumMember(Value = "registered")]
        Registered = 2,

        [EnumMember(Value = "draft")]
        Draft = 3
    }

    public class CreateDOIModel
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
    }

    public class ReadDOIModel
    {
        public DateTimeOffset CreateCreationDate { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long UserId { get; set; }
    }

    public class UpdateDOIModel
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long UserId { get; set; }
    }
}