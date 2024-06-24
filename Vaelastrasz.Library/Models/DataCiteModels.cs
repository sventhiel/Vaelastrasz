using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteContributor>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
            RightsList = new List<DataCiteRight>();
            Sizes = new List<string>();
            RelatedIdentifiers = new List<DataCiteRelatedIdentifier>();
            FundingReferences = new List<DataCiteFundingReference>();
        }

        [JsonProperty("contributors")]
        [XmlElement("contributors")]
        public List<DataCiteContributor> Contributors { get; set; }

        [Cardinality(Minimum = 1)]
        [JsonProperty("creators")]
        [XmlElement("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonProperty("dates")]
        [XmlElement("dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonProperty("descriptions")]
        [XmlElement("descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        [Required]
        [JsonProperty("doi")]
        [XmlElement("doi")]
        public string Doi { get; set; }

        [Required]
        [JsonProperty("event")]
        [XmlElement("event")]
        public DataCiteEventType Event { get; set; }

        [JsonProperty("fundingReferences")]
        [XmlElement("fundingReferences")]
        public List<DataCiteFundingReference> FundingReferences { get; set; }

        [JsonProperty("identifiers")]
        [XmlElement("identifiers")]
        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonProperty("language")]
        [XmlElement("language")]
        public string Language { get; set; }

        [Required]
        [JsonProperty("publicationYear")]
        [XmlElement("publicationYear")]
        public int PublicationYear { get; set; }

        [Required]
        [JsonProperty("publisher")]
        [XmlElement("publisher")]
        public DataCitePublisher Publisher { get; set; }

        [JsonProperty("relatedIdentifiers")]
        [XmlElement("relatedIdentifiers")]
        public List<DataCiteRelatedIdentifier> RelatedIdentifiers { get; set; }

        [JsonProperty("rightsList")]
        [XmlElement("rightsList")]
        public List<DataCiteRight> RightsList { get; set; }

        [JsonProperty("sizes")]
        [XmlElement("sizes")]
        public List<string> Sizes { get; set; }

        [JsonProperty("subjects")]
        [XmlElement("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [Cardinality(Minimum = 1)]
        [JsonProperty("titles")]
        [XmlElement("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [Required]
        [JsonProperty("types")]
        [XmlElement("types")]
        public DataCiteTypes Types { get; set; }

        [Required]
        [JsonProperty("url")]
        [XmlElement("url")]
        public string URL { get; set; }

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