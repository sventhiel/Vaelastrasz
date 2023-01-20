using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Vaelastrasz.Library.Attributes;
using Vaelastrasz.Library.Converters;
using Vaelastrasz.Library.Models.DataCite;

namespace Vaelastrasz.Library.Models
{
    public class CreateDataCiteModel
    {
        #region data

        [JsonPropertyName("data")]
        public DataCiteDataModel Data { get; set; }

        #endregion

        public static CreateDataCiteModel Deserialize(string json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
            };

            return JsonConvert.DeserializeObject<CreateDataCiteModel>(json, jsonSettings);
        }
    }

    public class ReadDataCiteModel
    {
        #region data

        [JsonProperty("data.id")]
        public string Id { get; set; }

        [JsonProperty("data.type")]
        public DataCiteType Type { get; set; }

        #region data.attributes

        [JsonProperty("data.attributes.doi")]
        public string Doi { get; set; }

        [JsonProperty("data.attributes.prefix")]
        public string Prefix { get; set; }

        [JsonProperty("data.attributes.suffix")]
        public string Suffix { get; set; }

        [JsonProperty("data.attributes.state")]
        public DataCiteStateType State { get; set; }

        [JsonProperty("data.attributes.identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("data.attributes.creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("data.attributes.titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonProperty("data.attributes.publisher")]
        public string Publisher { get; set; }

        [JsonProperty("data.attributes.publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("data.attributes.subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("data.attributes.contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonProperty("data.attributes.dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("data.attributes.language")]
        public string Language { get; set; }

        #region data.attributes.types

        [JsonProperty("data.attributes.types.resourceTypeGeneral")]
        public DataCiteResourceType ResourceTypeGeneral { get; set; }

        [JsonProperty("data.attributes.types.resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("data.attributes.types.schemaOrg")]
        public string SchemaOrg { get; set; }

        [JsonProperty("data.attributes.types.bibtex")]
        public string Bibtex { get; set; }

        [JsonProperty("data.attributes.types.citeproc")]
        public string Citeproc { get; set; }

        [JsonProperty("data.attributes.types.ris")]
        public string Ris { get; set; }

        #endregion data.attributes.types

        // Related Identifiers
        [JsonProperty("data.attributes.relatedIdentifiers")]
        public List<DataCiteRelatedIdentifier> RelatedIdentifiers { get; set; }

        [JsonProperty("data.attributes.sizes")]
        public List<string> Sizes { get; set; }

        [JsonProperty("data.attributes.formats")]
        public List<string> Formats { get; set; }

        [JsonProperty("data.attributes.version")]
        public string Version { get; set; }

        [JsonProperty("data.attributes.rightsList")]
        public List<DataCiteRight> Rights { get; set; }

        [JsonProperty("data.attributes.descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        [JsonProperty("data.attributes.geoLocations")]
        public List<DataCiteGeoLocation> GeoLocations { get; set; }

        [JsonProperty("data.attributes.fundingReferences")]
        public List<DataCiteFundingReference> FundingReferences { get; set; }

        [JsonProperty("data.attributes.url")]
        public string URL { get; set; }

        [JsonProperty("data.attributes.contentUrl")]
        public List<string> ContentUrl { get; set; }

        [JsonProperty("data.attributes.metadataVersion")]
        public int MetadataVersion { get; set; }

        [JsonProperty("data.attributes.schemaVersion")]
        public string SchemaVersion { get; set; }

        [JsonProperty("data.attributes.source")]
        public string Source { get; set; }

        [JsonProperty("data.attributes.isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("data.attributes.reason")]
        public string Reason { get; set; }

        #endregion data.attributes

        #endregion data

        public ReadDataCiteModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteCreator>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
            FundingReferences = new List<DataCiteFundingReference>();
            ContentUrl = new List<string>();
        }

        public static ReadDataCiteModel Deserialize(string json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            return JsonConvert.DeserializeObject<ReadDataCiteModel>(json, jsonSettings);
        }
    }

    public class UpdateDataCiteModel
    {
        #region data

        [JsonProperty("data.id")]
        public string Id { get; set; }

        [JsonProperty("data.type")]
        public DataCiteType Type { get; set; }

        #region data.attributes

        [JsonProperty("data.attributes.doi")]
        public string Doi { get; set; }

        [JsonProperty("data.attributes.prefix")]
        public string Prefix { get; set; }

        [JsonProperty("data.attributes.suffix")]
        public string Suffix { get; set; }

        [JsonProperty("data.attributes.event")]
        public DataCiteEventType Event { get; set; }

        [JsonProperty("data.attributes.identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("data.attributes.creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("data.attributes.titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonProperty("data.attributes.publisher")]
        public string Publisher { get; set; }

        [JsonProperty("data.attributes.publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("data.attributes.subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("data.attributes.contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonProperty("data.attributes.dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("data.attributes.language")]
        public string Language { get; set; }

        #region data.attributes.types

        [JsonProperty("data.attributes.types.resourceTypeGeneral")]
        public DataCiteResourceType ResourceTypeGeneral { get; set; }

        [JsonProperty("data.attributes.types.resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("data.attributes.types.schemaOrg")]
        public string SchemaOrg { get; set; }

        [JsonProperty("data.attributes.types.bibtex")]
        public string Bibtex { get; set; }

        [JsonProperty("data.attributes.types.citeproc")]
        public string Citeproc { get; set; }

        [JsonProperty("data.attributes.types.ris")]
        public string Ris { get; set; }

        #endregion data.attributes.types

        // Related Identifiers

        [JsonProperty("data.attributes.version")]
        public string Version { get; set; }

        [JsonProperty("data.attributes.url")]
        public string URL { get; set; }

        [JsonProperty("data.attributes.descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        #endregion data.attributes

        #endregion data

        public UpdateDataCiteModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteCreator>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
        }

        public static UpdateDataCiteModel Deserialize(string json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
            };

            return JsonConvert.DeserializeObject<UpdateDataCiteModel>(json, jsonSettings);
        }
    }

    public enum DataCiteStateType
    {
        [EnumMember(Value = "findable")]
        Findable = 1,

        [EnumMember(Value = "registered")]
        Registered = 2,

        [EnumMember(Value = "draft")]
        Draft = 3
    }
}