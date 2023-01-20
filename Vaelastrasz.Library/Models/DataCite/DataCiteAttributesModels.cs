using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAttributesModel
    {
        public string Doi { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public DataCiteEventType Event { get; set; }

        public List<DataCiteIdentifier> Identifiers { get; set; }

        public List<DataCiteCreator> Creators { get; set; }

        public List<DataCiteTitle> Titles { get; set; }

        public string Publisher { get; set; }

        public int PublicationYear { get; set; }

        public List<DataCiteSubject> Subjects { get; set; }

        public List<DataCiteCreator> Contributors { get; set; }

        public List<DataCiteDate> Dates { get; set; }

        public string Language { get; set; }

        public string Version { get; set; }

        public string URL { get; set; }

        public List<DataCiteDescription> Descriptions { get; set; }

        public DataCiteAttributesModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteCreator>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
        }
    }

    public enum DataCiteEventType
    {
        [EnumMember(Value = "publish")]
        Publish = 1,

        [EnumMember(Value = "register")]
        Register = 2,

        [EnumMember(Value = "hide")]
        Hide = 3
    }
}
