using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedIdentifierModel
    {
        [JsonPropertyName("relatedIdentifier")]
        public string RelatedIdentifier { get; set; }

        [JsonPropertyName("relatedIdentifierType")]
        public DataCiteRelatedIdentifierType RelatedIdentifierType { get; set; }

        [JsonPropertyName("relationType")]
        public DataCiteRelationType RelationType { get; set; }

        [JsonPropertyName("resourceTypeGeneral")]
        public string ResourceTypeGeneral { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteRelatedIdentifierType
    {
        [JsonPropertyName("ARK")]
        ARK = 1,

        [JsonPropertyName("arXiv")]
        arXiv = 2,

        [JsonPropertyName("bibcode")]
        bibcode = 3,

        [JsonPropertyName("DOI")]
        DOI = 4,

        [JsonPropertyName("EAN13")]
        EAN13 = 5,

        [JsonPropertyName("EISSN")]
        EISSN = 6,

        [JsonPropertyName("Handle")]
        Handle = 7,

        [JsonPropertyName("IGSN")]
        IGSN = 8,

        [JsonPropertyName("ISBN")]
        ISBN = 9,

        [JsonPropertyName("ISSN")]
        ISSN = 10,

        [JsonPropertyName("ISTC")]
        ISTC = 11,

        [JsonPropertyName("LISSN")]
        LISSN = 12,

        [JsonPropertyName("LSID")]
        LSID = 13,

        [JsonPropertyName("PMID")]
        PMID = 14,

        [JsonPropertyName("PURL")]
        PURL = 15,

        [JsonPropertyName("UPC")]
        UPC = 16,

        [JsonPropertyName("URL")]
        URL = 17,

        [JsonPropertyName("URN")]
        URN = 18,

        [JsonPropertyName("w3id")]
        w3id = 19
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteRelationType
    {
        [JsonPropertyName("IsCitedBy")]
        IsCitedBy = 1,

        [JsonPropertyName("Cites")]
        Cites = 2,

        [JsonPropertyName("IsSupplementTo")]
        IsSupplementTo = 3,

        [JsonPropertyName("IsSupplementedBy")]
        IsSupplementedBy = 4,

        [JsonPropertyName("IsContinuedBy")]
        IsContinuedBy = 5,

        [JsonPropertyName("Continues")]
        Continues = 6,

        [JsonPropertyName("IsDescribedBy")]
        IsDescribedBy = 7,

        [JsonPropertyName("Describes")]
        Describes = 8,

        [JsonPropertyName("HasMetadata")]
        HasMetadata = 9,

        [JsonPropertyName("IsMetadataFor")]
        IsMetadataFor = 10,

        [JsonPropertyName("HasVersion")]
        HasVersion = 11,

        [JsonPropertyName("IsVersionOf")]
        IsVersionOf = 12,

        [JsonPropertyName("IsNewVersionOf")]
        IsNewVersionOf = 13,

        [JsonPropertyName("IsPreviousVersionOf")]
        IsPreviousVersionOf = 14,

        [JsonPropertyName("IsPartOf")]
        IsPartOf = 15,

        [JsonPropertyName("HasPart")]
        HasPart = 16,

        [JsonPropertyName("IsPublishedIn")]
        IsPublishedIn = 17,

        [JsonPropertyName("IsReferencedBy")]
        IsReferencedBy = 18,

        [JsonPropertyName("References")]
        References = 19,

        [JsonPropertyName("IsDocumentedBy")]
        IsDocumentedBy = 20,

        [JsonPropertyName("Documents")]
        Documents = 21,

        [JsonPropertyName("IsCompiledBy")]
        IsCompiledBy = 22,

        [JsonPropertyName("Compiles")]
        Compiles = 23,

        [JsonPropertyName("IsVariantFormOf")]
        IsVariantFormOf = 24,

        [JsonPropertyName("IsOriginalFormOf")]
        IsOriginalFormOf = 25,

        [JsonPropertyName("IsIdenticalTo")]
        IsIdenticalTo = 26,

        [JsonPropertyName("IsReviewedBy")]
        IsReviewedBy = 27,

        [JsonPropertyName("Reviews")]
        Reviews = 28,

        [JsonPropertyName("IsDerivedFrom")]
        IsDerivedFrom = 29,

        [JsonPropertyName("IsSourceOf")]
        IsSourceOf = 30,

        [JsonPropertyName("w3IsRequiredByid")]
        IsRequiredBy = 31,

        [JsonPropertyName("Requires")]
        Requires = 32,

        [JsonPropertyName("IsObsoletedBy")]
        IsObsoletedBy = 33,

        [JsonPropertyName("Obsoletes")]
        Obsoletes = 34
    }
}