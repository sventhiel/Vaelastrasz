using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vaelastrasz.Library.Models.DataCite;

namespace Vaelastrasz.Library.Models
{
    #region create

    public class CreateDataCiteModel
    {
        [JsonPropertyName("data")]
        public CreateDataCiteDataModel Data { get; set; }
    }

    public class CreateDataCiteDataModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public DataCiteType Type { get; set; }

        [JsonPropertyName("attributes")]
        public CreateDataCiteAttributesModel Attributes { get; set; }
    }

    public class CreateDataCiteAttributesModel
    {
        [JsonPropertyName("doi")]
        public string Doi { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("suffix")]
        public string Suffix { get; set; }

        [JsonPropertyName("event")]
        public DataCiteEventType Event { get; set; }

        [JsonPropertyName("identifiers")]
        public List<DataCiteIdentifierModel> Identifiers { get; set; }

        [JsonPropertyName("creators")]
        public List<DataCiteCreatorModel> Creators { get; set; }

        [JsonPropertyName("titles")]
        public List<DataCiteTitleModel> Titles { get; set; }

        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        [JsonPropertyName("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonPropertyName("subjects")]
        public List<DataCiteSubjectModel> Subjects { get; set; }

        [JsonPropertyName("contributors")]
        public List<DataCiteCreatorModel> Contributors { get; set; }

        [JsonPropertyName("dates")]
        public List<DataCiteDateModel> Dates { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }

        [JsonPropertyName("descriptions")]
        public List<DataCiteDescriptionModel> Descriptions { get; set; }

        public CreateDataCiteAttributesModel()
        {
            Creators = new List<DataCiteCreatorModel>();
            Contributors = new List<DataCiteCreatorModel>();
            Dates = new List<DataCiteDateModel>();
            Descriptions = new List<DataCiteDescriptionModel>();
            Identifiers = new List<DataCiteIdentifierModel>();
            Subjects = new List<DataCiteSubjectModel>();
            Titles = new List<DataCiteTitleModel>();
        }
    }

    #endregion

    #region update

    public class UpdateDataCiteModel
    {
        [JsonPropertyName("data")]
        public UpdateDataCiteDataModel Data { get; set; }
    }

    public class UpdateDataCiteDataModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public DataCiteType Type { get; set; }

        [JsonPropertyName("attributes")]
        public UpdateDataCiteAttributesModel Attributes { get; set; }
    }

    public class UpdateDataCiteAttributesModel
    {
        [JsonPropertyName("doi")]
        public string Doi { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("suffix")]
        public string Suffix { get; set; }

        [JsonPropertyName("event")]
        public DataCiteEventType Event { get; set; }

        [JsonPropertyName("identifiers")]
        public List<DataCiteIdentifierModel> Identifiers { get; set; }

        [JsonPropertyName("creators")]
        public List<DataCiteCreatorModel> Creators { get; set; }

        [JsonPropertyName("titles")]
        public List<DataCiteTitleModel> Titles { get; set; }

        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        [JsonPropertyName("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonPropertyName("subjects")]
        public List<DataCiteSubjectModel> Subjects { get; set; }

        [JsonPropertyName("contributors")]
        public List<DataCiteCreatorModel> Contributors { get; set; }

        [JsonPropertyName("dates")]
        public List<DataCiteDateModel> Dates { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }
    }

    #endregion

    #region read

    public class ReadDataCiteModel
    {
        [JsonPropertyName("data")]
        public ReadDataCiteDataModel Data { get; set; }
    }

    public class ReadDataCiteDataModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public DataCiteType Type { get; set; }

        [JsonPropertyName("attributes")]
        public ReadDataCiteAttributesModel Attributes { get; set; }
    }

    public class ReadDataCiteAttributesModel
    {
        [JsonPropertyName("doi")]
        public string Doi { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("suffix")]
        public string Suffix { get; set; }

        [JsonPropertyName("status")]
        public DataCiteStatusType Status { get; set; }

        [JsonPropertyName("identifiers")]
        public List<DataCiteIdentifierModel> Identifiers { get; set; }

        [JsonPropertyName("creators")]
        public List<DataCiteCreatorModel> Creators { get; set; }

        [JsonPropertyName("titles")]
        public List<DataCiteTitleModel> Titles { get; set; }

        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        [JsonPropertyName("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonPropertyName("subjects")]
        public List<DataCiteSubjectModel> Subjects { get; set; }

        [JsonPropertyName("contributors")]
        public List<DataCiteCreatorModel> Contributors { get; set; }

        [JsonPropertyName("dates")]
        public List<DataCiteDateModel> Dates { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }
    }

    #endregion

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteType
    {
        [JsonPropertyName("dois")]
        DOIs = 1
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteEventType
    {
        [JsonPropertyName("publish")]
        Publish = 1,

        [JsonPropertyName("register")]
        Register = 2,

        [JsonPropertyName("hide")]
        Hide = 3
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteStatusType
    {
        [JsonPropertyName("findable")]
        Findable = 1,

        [JsonPropertyName("registered")]
        Registered = 2,

        [JsonPropertyName("draft")]
        Draft = 3
    } 

    //
    // Delete

    // not required, because delete will be executed by id - only.
}