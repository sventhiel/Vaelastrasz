using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Vaelastrasz.Library.Attributes;
using Vaelastrasz.Library.Models.DataCite;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models
{
    #region create

    public class CreateDataCiteAttributesModel
    {
        public CreateDataCiteAttributesModel()
        {
            GeoLocations = new List<DataCiteGeoLocation>();
            AlternateIdentifiers = new List<DataCiteAlternateIdentifier>();
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteContributor>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
            RightsList = new List<DataCiteRight>();
            Sizes = new List<string>();
            Formats = new List<string>();
            RelatedIdentifiers = new List<DataCiteRelatedIdentifier>();
            FundingReferences = new List<DataCiteFundingReference>();
        }

        // 12 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/alternateidentifier/
        [JsonProperty("alternateIdentifiers")]
        [XmlElement("alternateIdentifiers")]
        public List<DataCiteAlternateIdentifier> AlternateIdentifiers { get; set; }

        // 7 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/contributor/
        [JsonProperty("contributors")]
        [XmlElement("contributors")]
        public List<DataCiteContributor> Contributors { get; set; }

        // 18 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/geolocation/
        [JsonProperty("geoLocations")]
        [XmlElement("geoLocations")]
        public List<DataCiteGeoLocation> GeoLocations { get; set; }

        // 2 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/creator/
        [Cardinality(Minimum = 1)]
        [JsonProperty("creators")]
        [XmlElement("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        // 8 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/date/
        [JsonProperty("dates")]
        [XmlElement("dates")]
        public List<DataCiteDate> Dates { get; set; }

        // 17 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/description/
        [JsonProperty("descriptions")]
        [XmlElement("descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        // 0
        [Required]
        [JsonProperty("doi")]
        [XmlElement("doi")]
        public string Doi { get; set; }

        // 0
        [Required]
        [JsonProperty("event")]
        [XmlElement("event")]
        public DataCiteEventType Event { get; set; }

        // 14 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/format/
        [JsonProperty("formats")]
        [XmlElement("formats")]
        public List<string> Formats { get; set; }

        // 19 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/fundingreference/
        [JsonProperty("fundingReferences")]
        [XmlElement("fundingReferences")]
        public List<DataCiteFundingReference> FundingReferences { get; set; }

        // 1 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/identifier/
        [JsonProperty("identifiers")]
        [XmlElement("identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        // 9 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/language/
        [JsonProperty("language")]
        [XmlElement("language")]
        public string Language { get; set; }

        // 5 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/publicationyear/
        [Required]
        [JsonProperty("publicationYear")]
        [XmlElement("publicationYear")]
        public int PublicationYear { get; set; }

        // 4 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/publisher/
        [Required]
        [JsonProperty("publisher")]
        [XmlElement("publisher")]
        public DataCitePublisher Publisher { get; set; }

        // 12 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/relatedidentifier/
        [JsonProperty("relatedIdentifiers")]
        [XmlElement("relatedIdentifiers")]
        public List<DataCiteRelatedIdentifier> RelatedIdentifiers { get; set; }

        // 16 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/rights/#
        [JsonProperty("rightsList")]
        [XmlElement("rightsList")]
        public List<DataCiteRight> RightsList { get; set; }

        // 13 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/size/
        [JsonProperty("sizes")]
        [XmlElement("sizes")]
        public List<string> Sizes { get; set; }

        // 6 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/subject/
        [JsonProperty("subjects")]
        [XmlElement("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        // 3 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/title/
        [Cardinality(Minimum = 1)]
        [JsonProperty("titles")]
        [XmlElement("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        // 10 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/resourcetype/
        [Required]
        [JsonProperty("types")]
        [XmlElement("types")]
        public DataCiteTypes Types { get; set; }

        // 0
        [Required]
        [JsonProperty("url")]
        [XmlElement("url")]
        public string URL { get; set; }

        // 15 - https://datacite-metadata-schema.readthedocs.io/en/4.5/properties/version/
        [JsonProperty("version")]
        [XmlElement("version")]
        public string Version { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class CreateDataCiteDataModel
    {
        public CreateDataCiteDataModel()
        {
            Attributes = new CreateDataCiteAttributesModel();
        }

        [Required]
        [JsonProperty("attributes")]
        [XmlElement("attributes")]
        public CreateDataCiteAttributesModel Attributes { get; set; }

        [Required]
        [JsonProperty("type")]
        [XmlElement("type")]
        public DataCiteType Type { get; set; }
    }

    /// <summary>
    /// This is the general model to create a doi at DataCite.
    /// </summary>
    public class CreateDataCiteModel
    {
        /// <summary>
        /// There is the necessity of the 'data' root node.
        /// </summary>
        public CreateDataCiteModel()
        {
            Data = new CreateDataCiteDataModel();
        }

        [Required]
        [JsonProperty("data")]
        public CreateDataCiteDataModel Data { get; set; }
    }

    #endregion create

    #region read

    public class ReadDataCiteAttributesModel
    {
        public ReadDataCiteAttributesModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteContributor>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
            RightsList = new List<DataCiteRight>();
            Sizes = new List<string>();
            Formats = new List<string>();
            RelatedIdentifiers = new List<DataCiteRelatedIdentifier>();
            FundingReferences = new List<DataCiteFundingReference>();
        }

        [JsonProperty("contributors")]
        public List<DataCiteContributor> Contributors { get; set; }

        [JsonProperty("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        [JsonProperty("doi")]
        public string Doi { get; set; }

        [JsonProperty("formats")]
        public List<string> Formats { get; set; }

        [JsonProperty("fundingReferences")]
        public List<DataCiteFundingReference> FundingReferences { get; set; }

        [JsonProperty("identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("relatedIdentifiers")]
        public List<DataCiteRelatedIdentifier> RelatedIdentifiers { get; set; }

        [JsonProperty("rightsList")]
        public List<DataCiteRight> RightsList { get; set; }

        [JsonProperty("sizes")]
        public List<string> Sizes { get; set; }

        [JsonProperty("state")]
        public DataCiteStateType State { get; set; }

        [JsonProperty("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonProperty("types")]
        public DataCiteTypes Types { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class ReadDataCiteDataModel
    {
        public ReadDataCiteDataModel()
        {
            Attributes = new ReadDataCiteAttributesModel();
        }

        [JsonProperty("attributes")]
        public ReadDataCiteAttributesModel Attributes { get; set; }

        [JsonProperty("type")]
        public DataCiteType? Type { get; set; }
    }

    public class ReadDataCiteModel
    {
        public ReadDataCiteModel()
        {
            Data = new ReadDataCiteDataModel();
        }

        [JsonProperty("data")]
        public ReadDataCiteDataModel Data { get; set; }
    }

    //public class ReadListDataCiteModel
    //{
    //    [JsonProperty("data")]
    //    public List<ReadDataCiteDataModel> Data { get; set; }

    //    public ReadListDataCiteModel()
    //    {
    //        Data = new List<ReadDataCiteDataModel>();
    //    }
    //}

    #endregion read

    #region update

    public class UpdateDataCiteAttributesModel
    {
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

        [JsonProperty("contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonProperty("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        [JsonProperty("doi")]
        public string Doi { get; set; }

        [JsonProperty("event")]
        public DataCiteEventType Event { get; set; }

        [JsonProperty("identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("relatedIdentifiers")]
        public List<DataCiteRelatedIdentifier> RelatedIdentifiers { get; set; }

        [JsonProperty("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonProperty("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonProperty("types")]
        public DataCiteTypes Types { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class UpdateDataCiteDataModel
    {
        public UpdateDataCiteDataModel()
        {
            Attributes = new UpdateDataCiteAttributesModel();
        }

        [JsonProperty("attributes")]
        public UpdateDataCiteAttributesModel Attributes { get; set; }

        [JsonProperty("type")]
        public DataCiteType? Type { get; set; }
    }

    public class UpdateDataCiteModel
    {
        public UpdateDataCiteModel()
        {
            Data = new UpdateDataCiteDataModel();
        }

        [JsonProperty("data")]
        public UpdateDataCiteDataModel Data { get; set; }
    }

    #endregion update
}