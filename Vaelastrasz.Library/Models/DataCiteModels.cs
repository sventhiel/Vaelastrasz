using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Vaelastrasz.Library.Models.DataCite;

namespace Vaelastrasz.Library.Models
{
    //public class CreateDataCiteModel
    //{
    //    #region data

    //    [JsonPropertyName("data.type")]
    //    public DataCiteType Type { get; set; }

    //    #region data.attributes

    //    [JsonPropertyName("data.attributes.doi")]
    //    public string Doi { get; set; }

    //    [JsonPropertyName("data.attributes.event")]
    //    public DataCiteEventType Event { get; set; }

    //    [JsonPropertyName("data.attributes.identifiers")]
    //    public List<DataCiteIdentifier> Identifiers { get; set; }

    //    [JsonPropertyName("data.attributes.creators")]
    //    public List<DataCiteCreator> Creators { get; set; }

    //    [JsonPropertyName("data.attributes.titles")]
    //    public List<DataCiteTitle> Titles { get; set; }

    //    [JsonPropertyName("data.attributes.publisher")]
    //    public string Publisher { get; set; }

    //    [JsonPropertyName("data.attributes.publicationYear")]
    //    public int PublicationYear { get; set; }

    //    [JsonPropertyName("data.attributes.subjects")]
    //    public List<DataCiteSubject> Subjects { get; set; }

    //    [JsonPropertyName("data.attributes.contributors")]
    //    public List<DataCiteCreator> Contributors { get; set; }

    //    [JsonPropertyName("data.attributes.dates")]
    //    public List<DataCiteDate> Dates { get; set; }

    //    [JsonPropertyName("data.attributes.language")]
    //    public string Language { get; set; }

    //    #region data.attributes.types

    //    [JsonPropertyName("data.attributes.types.resourceTypeGeneral")]
    //    public DataCiteResourceType ResourceTypeGeneral { get; set; }

    //    [JsonPropertyName("data.attributes.types.resourceType")]
    //    public string ResourceType { get; set; }

    //    [JsonPropertyName("data.attributes.types.schemaOrg")]
    //    public string SchemaOrg { get; set; }

    //    [JsonPropertyName("data.attributes.types.bibtex")]
    //    public string Bibtex { get; set; }

    //    [JsonPropertyName("data.attributes.types.citeproc")]
    //    public string Citeproc { get; set; }

    //    [JsonPropertyName("data.attributes.types.ris")]
    //    public string Ris { get; set; }

    //    #endregion data.attributes.types

    //    // Related Identifiers

    //    [JsonPropertyName("data.attributes.version")]
    //    public string Version { get; set; }

    //    [JsonPropertyName("data.attributes.url")]
    //    public string URL { get; set; }

    //    [JsonPropertyName("data.attributes.descriptions")]
    //    public List<DataCiteDescription> Descriptions { get; set; }

    //    #endregion data.attributes

    //    #endregion data

    //    [Newtonsoft.Json.JsonConstructor]
    //    public CreateDataCiteModel()
    //    {
    //        Creators = new List<DataCiteCreator>();
    //        Contributors = new List<DataCiteCreator>();
    //        Dates = new List<DataCiteDate>();
    //        Descriptions = new List<DataCiteDescription>();
    //        Identifiers = new List<DataCiteIdentifier>();
    //        Subjects = new List<DataCiteSubject>();
    //        Titles = new List<DataCiteTitle>();
    //    }

    //    public static CreateDataCiteModel Deserialize(string json)
    //    {
    //        var jsonSettings = new JsonSerializerSettings
    //        {
    //            NullValueHandling = NullValueHandling.Ignore,
    //            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
    //        };

    //        return JsonConvert.DeserializeObject<CreateDataCiteModel>(json, jsonSettings);
    //    }
    //}

    #region creation

    public class CreateDataCiteModel
    {
        [JsonPropertyName("data")]
        public CreateDataCiteDataModel Data { get; set; }
    }

    public class CreateDataCiteDataModel
    {
        [JsonPropertyName("type")]
        public DataCiteType Type { get; set; }

        [JsonPropertyName("attributes")]
        public CreateDataCiteAttributesModel Attributes { get; set; }
    }

    public class CreateDataCiteAttributesModel
    {
        [JsonPropertyName("doi")]
        public string Doi { get; set; }

        [JsonPropertyName("event")]
        public DataCiteEventType Event { get; set; }

        [JsonPropertyName("identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonPropertyName("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonPropertyName("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        [JsonPropertyName("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonPropertyName("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonPropertyName("contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonPropertyName("dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }

        [JsonPropertyName("descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        public CreateDataCiteAttributesModel()
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

    #endregion creation

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataCiteEventType
    {
        [EnumMember(Value = "publish")]
        Publish = 1,

        [EnumMember(Value = "register")]
        Register = 2,

        [EnumMember(Value = "hide")]
        Hide = 3
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataCiteStateType
    {
        [EnumMember(Value = "findable")]
        Findable = 1,

        [EnumMember(Value = "registered")]
        Registered = 2,

        [EnumMember(Value = "draft")]
        Draft = 3
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataCiteResourceType
    {
        [EnumMember(Value = "Audiovisual")]
        Audiovisual = 1,

        [EnumMember(Value = "Book")]
        Book = 2,

        BookChapter = 3,
        Collection = 4,
        ComputationalNotebook = 5,
        ConferencePaper = 6,
        ConferenceProceeding = 7,
        DataPaper = 8,
        Dataset = 9,
        Dissertation = 10,
        Event = 11,
        Image = 12,
        InteractiveResource = 13,
        JournalArticle = 14,
        Model = 15,
        OutputManagementPlan = 16,
        PeerReview = 17,
        PhysicalObject = 18,
        Preprint = 19,
        Report = 20,
        Service = 21,
        Software = 22,
        Sound = 23,
        Standard = 24,
        Text = 25,
        Workflow = 26,
        Other = 27
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataCiteType
    {
        [EnumMember(Value = "dois")]
        DOIs = 1
    }
}