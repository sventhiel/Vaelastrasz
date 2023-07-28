using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RelatedIdentifierType
    {
        [EnumMember(Value = "ARK")]
        ARK = 1,

        [EnumMember(Value = "arXiv")]
        arXiv = 2,

        [EnumMember(Value = "bibcode")]
        bibcode = 3,

        [EnumMember(Value = "DOI")]
        DOI = 4,

        [EnumMember(Value = "EAN13")]
        EAN13 = 5,

        [EnumMember(Value = "EISSN")]
        EISSN = 6,

        [EnumMember(Value = "Handle")]
        Handle = 7,

        [EnumMember(Value = "IGSN")]
        IGSN = 8,

        [EnumMember(Value = "ISBN")]
        ISBN = 9,

        [EnumMember(Value = "ISSN")]
        ISSN = 10,

        [EnumMember(Value = "ISTC")]
        ISTC = 11,

        [EnumMember(Value = "LISSN")]
        LISSN = 12,

        [EnumMember(Value = "LSID")]
        LSID = 13,

        [EnumMember(Value = "PMID")]
        PMID = 14,

        [EnumMember(Value = "PURL")]
        PURL = 15,

        [EnumMember(Value = "UPC")]
        UPC = 16,

        [EnumMember(Value = "URL")]
        URL = 17,

        [EnumMember(Value = "URN")]
        URN = 18,

        [EnumMember(Value = "w3id")]
        w3id = 19
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RelationType
    {
        [EnumMember(Value = "IsCitedBy")]
        IsCitedBy = 1,

        [EnumMember(Value = "Cites")]
        Cites = 2,

        [EnumMember(Value = "IsSupplementTo")]
        IsSupplementTo = 3,

        [EnumMember(Value = "IsSupplementedBy")]
        IsSupplementedBy = 4,

        [EnumMember(Value = "IsContinuedBy")]
        IsContinuedBy = 5,

        [EnumMember(Value = "Continues")]
        Continues = 6,

        [EnumMember(Value = "IsDescribedBy")]
        IsDescribedBy = 7,

        [EnumMember(Value = "Describes")]
        Describes = 8,

        [EnumMember(Value = "HasMetadata")]
        HasMetadata = 9,

        [EnumMember(Value = "IsMetadataFor")]
        IsMetadataFor = 10,

        [EnumMember(Value = "HasVersion")]
        HasVersion = 11,

        [EnumMember(Value = "IsVersionOf")]
        IsVersionOf = 12,

        [EnumMember(Value = "IsNewVersionOf")]
        IsNewVersionOf = 13,

        [EnumMember(Value = "IsPreviousVersionOf")]
        IsPreviousVersionOf = 14,

        [EnumMember(Value = "IsPartOf")]
        IsPartOf = 15,

        [EnumMember(Value = "HasPart")]
        HasPart = 16,

        [EnumMember(Value = "IsPublishedIn")]
        IsPublishedIn = 17,

        [EnumMember(Value = "IsReferencedBy")]
        IsReferencedBy = 18,

        [EnumMember(Value = "References")]
        References = 19,

        [EnumMember(Value = "IsDocumentedBy")]
        IsDocumentedBy = 20,

        [EnumMember(Value = "Documents")]
        Documents = 21,

        [EnumMember(Value = "IsCompiledBy")]
        IsCompiledBy = 22,

        [EnumMember(Value = "Compiles")]
        Compiles = 23,

        [EnumMember(Value = "IsVariantFormOf")]
        IsVariantFormOf = 24,

        [EnumMember(Value = "IsOriginalFormOf")]
        IsOriginalFormOf = 25,

        [EnumMember(Value = "IsIdenticalTo")]
        IsIdenticalTo = 26,

        [EnumMember(Value = "IsReviewedBy")]
        IsReviewedBy = 27,

        [EnumMember(Value = "Reviews")]
        Reviews = 28,

        [EnumMember(Value = "IsDerivedFrom")]
        IsDerivedFrom = 29,

        [EnumMember(Value = "IsSourceOf")]
        IsSourceOf = 30,

        [EnumMember(Value = "IsRequiredBy")]
        IsRequiredBy = 31,

        [EnumMember(Value = "Requires")]
        Requires = 32,

        [EnumMember(Value = "IsObsoletedBy")]
        IsObsoletedBy = 33,

        [EnumMember(Value = "Obsoletes")]
        Obsoletes = 34
    }

    public class DataCiteRelatedIdentifier
    {
        [JsonProperty("relatedIdentifier")]
        [XmlElement("relatedIdentifier")]
        public string RelatedIdentifier { get; set; }

        [JsonProperty("relatedIdentifierType")]
        [XmlElement("relatedIdentifierType")]
        public RelatedIdentifierType RelatedIdentifierType { get; set; }

        [JsonProperty("relationType")]
        [XmlElement("relationType")]
        public RelationType RelationType { get; set; }

        [JsonProperty("resourceTypeGeneral")]
        [XmlElement("resourceTypeGeneral")]
        public string ResourceTypeGeneral { get; set; }
    }
}