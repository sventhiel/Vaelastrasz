using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vaelastrasz.Library.Models.DataCite;

namespace Vaelastrasz.Library.Models
{
    #region create

    public class CreateDataCiteModel
    {
        [JsonProperty("data")]
        public CreateDataCiteDataModel Data { get; set; }

        public CreateDataCiteModel()
        {
            Data = new CreateDataCiteDataModel();
        }
    }

    public class CreateDataCiteDataModel
    {
        [JsonProperty("type")]
        public DataCiteType? Type { get; set; }

        [JsonProperty("attributes")]
        public CreateDataCiteAttributesModel Attributes { get; set; }

        public CreateDataCiteDataModel()
        {
            Attributes = new CreateDataCiteAttributesModel();
        }
    }

    public class CreateDataCiteAttributesModel
    {
        [JsonProperty("doi")]
        public string Doi { get; set; }

        [JsonProperty("event")]
        public DataCiteEventType Event { get; set; }

        [JsonProperty("identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonProperty("dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("types")]
        public DataCiteTypes Types { get; set; }

        [JsonProperty("relatedIdentifiers")]
        public List<DataCiteRelatedIdentifier> RelatedIdentifiers { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("descriptions")]
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
            RelatedIdentifiers = new List<DataCiteRelatedIdentifier>();
            
        }
    }

    #endregion create

    #region read
    public class ReadDataCiteModel
    {
        [JsonProperty("data")]
        public ReadDataCiteDataModel Data { get; set; }

        public ReadDataCiteModel()
        {
            Data = new ReadDataCiteDataModel();
        }
    }

    public class ReadDataCiteDataModel
    {
        [JsonProperty("type")]
        public DataCiteType? Type { get; set; }

        [JsonProperty("attributes")]
        public ReadDataCiteAttributesModel Attributes { get; set; }

        public ReadDataCiteDataModel()
        {
            Attributes = new ReadDataCiteAttributesModel();
        }
    }

    public class ReadDataCiteAttributesModel
    {
        [JsonProperty("doi")]
        public string Doi { get; set; }

        [JsonProperty("state")]
        public DataCiteEventType State { get; set; }

        [JsonProperty("identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonProperty("dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("types")]
        public DataCiteTypes Types { get; set; }

        [JsonProperty("relatedIdentifiers")]
        public List<DataCiteRelatedIdentifier> RelatedIdentifiers { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        public ReadDataCiteAttributesModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteCreator>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
            RelatedIdentifiers = new List<DataCiteRelatedIdentifier>();

        }
    }

    #endregion

    #region update

    public class UpdateDataCiteModel
    {
        [JsonProperty("data")]
        public UpdateDataCiteDataModel Data { get; set; }

        public UpdateDataCiteModel()
        {
            Data = new UpdateDataCiteDataModel();
        }
    }

    public class UpdateDataCiteDataModel
    {
        [JsonProperty("type")]
        public DataCiteType? Type { get; set; }

        [JsonProperty("attributes")]
        public UpdateDataCiteAttributesModel Attributes { get; set; }

        public UpdateDataCiteDataModel()
        {
            Attributes = new UpdateDataCiteAttributesModel();
        }
    }

    public class UpdateDataCiteAttributesModel
    {
        [JsonProperty("doi")]
        public string Doi { get; set; }

        [JsonProperty("event")]
        public DataCiteEventType Event { get; set; }

        [JsonProperty("identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonProperty("dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("types")]
        public DataCiteTypes Types { get; set; }

        [JsonProperty("relatedIdentifiers")]
        public List<DataCiteRelatedIdentifier> RelatedIdentifiers { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        public UpdateDataCiteAttributesModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteCreator>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
            RelatedIdentifiers = new List<DataCiteRelatedIdentifier>();

        }
    }

    #endregion

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteEventType
    {
        [EnumMember(Value = "publish")]
        Publish = 1,

        [EnumMember(Value = "register")]
        Register = 2,

        [EnumMember(Value = "hide")]
        Hide = 3
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteStateType
    {
        [EnumMember(Value = "findable")]
        Findable = 1,

        [EnumMember(Value = "registered")]
        Registered = 2,

        [EnumMember(Value = "draft")]
        Draft = 3
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteResourceType
    {
        [EnumMember(Value = "Audiovisual")]
        Audiovisual = 1,
        [EnumMember(Value = "Book")]
        Book = 2,
        [EnumMember(Value = "BookChapter")]
        BookChapter = 3,
        [EnumMember(Value = "Collection")]
        Collection = 4,
        [EnumMember(Value = "ComputationalNotebook")]
        ComputationalNotebook = 5,
        [EnumMember(Value = "ConferencePaper")]
        ConferencePaper = 6,
        [EnumMember(Value = "ConferenceProceeding")]
        ConferenceProceeding = 7,
        [EnumMember(Value = "DataPaper")]
        DataPaper = 8,
        [EnumMember(Value = "Dataset")]
        Dataset = 9,
        [EnumMember(Value = "Dissertation")]
        Dissertation = 10,
        [EnumMember(Value = "Event")]
        Event = 11,
        [EnumMember(Value = "Image")]
        Image = 12,
        [EnumMember(Value = "InteractiveResource")]
        InteractiveResource = 13,
        [EnumMember(Value = "JournalArticle")]
        JournalArticle = 14,
        [EnumMember(Value = "Model")]
        Model = 15,
        [EnumMember(Value = "OutputManagementPlan")]
        OutputManagementPlan = 16,
        [EnumMember(Value = "PeerReview")]
        PeerReview = 17,
        [EnumMember(Value = "PhysicalObject")]
        PhysicalObject = 18,
        [EnumMember(Value = "Preprint")]
        Preprint = 19,
        [EnumMember(Value = "Report")]
        Report = 20,
        [EnumMember(Value = "Service")]
        Service = 21,
        [EnumMember(Value = "Software")]
        Software = 22,
        [EnumMember(Value = "Sound")]
        Sound = 23,
        [EnumMember(Value = "Standard")]
        Standard = 24,
        [EnumMember(Value = "Text")]
        Text = 25,
        [EnumMember(Value = "Workflow")]
        Workflow = 26,
        [EnumMember(Value = "Other")]
        Other = 27
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteType
    {
        [EnumMember(Value = "dois")]
        DOIs = 1
    }
}