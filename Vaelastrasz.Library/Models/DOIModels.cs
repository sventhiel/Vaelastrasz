using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        public DOIStateType Status { get; set; }
        public long UserId { get; set; }
        public string Value { get; set; }
    }

    public class ReadDOIModel
    {
        public DateTimeOffset CreateCreationDate { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long UserId { get; set; }

        public DOIStateType State { get; set; }
    }

    public class UpdateDOIModel
    {
        public DOIStateType State { get; set; }

        public long UserId { get; set; }

        public static UpdateDOIModel Convert(UpdateDataCiteModel model, long userId)
        {
            return new UpdateDOIModel()
            {
            };
        }
    }
}