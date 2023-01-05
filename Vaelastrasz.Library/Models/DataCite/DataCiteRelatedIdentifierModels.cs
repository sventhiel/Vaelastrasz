using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class DataCiteRelatedIdentifier
    {
        [JsonProperty("relatedIdentifier")]
        public string RelatedIdentifier { get; set; }

        [JsonProperty("relatedIdentifierType")]
        public string RelatedIdentifierType { get; set; }

        [JsonProperty("relationType")]
        public string RelationType { get; set; }

        [JsonProperty("resourceTypeGeneral")]
        public string ResourceTypeGeneral { get; set; }
    }

    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
    public enum RelatedIdentifierType
    {
        ARK = 1,
        arXiv = 2,
        bibcode = 3,
        DOI = 4,
        EAN13 = 5,
        EISSN = 6,
        Handle = 7,
        IGSN = 8,
        ISBN =9,
        ISSN = 10,
        ISTC = 11,
        LISSN = 12,
        LSID = 13,
        PMID = 14,
        PURL = 15,
        UPC = 16,
        URL = 17,
        URN = 18,
        w3id = 19
    }

    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
    public enum RelationType
    {
        IsCitedBy = 1,
        Cites = 2,
        IsSupplementTo = 3,
        IsSupplementedBy = 4,
        IsContinuedBy = 5,
        Continues = 6,
        IsDescribedBy = 7,
        Describes = 8,
        HasMetadata = 9,
        IsMetadataFor = 10,
        HasVersion = 11,
        IsVersionOf = 12,
        IsNewVersionOf = 13,
        IsPreviousVersionOf = 14,
        IsPartOf = 15,
        HasPart = 16,
        IsPublishedIn = 17,
        IsReferencedBy = 18,
        References = 19,
        IsDocumentedBy = 20,
        Documents = 21,
        IsCompiledBy = 22,
        Compiles = 23,
        IsVariantFormOf = 24,
        IsOriginalFormOf = 25,
        IsIdenticalTo = 26,
        IsReviewedBy = 27,
        Reviews = 28,
        IsDerivedFrom = 29,
        IsSourceOf = 30,
        IsRequiredBy = 31,
        Requires = 32,
        IsObsoletedBy = 33,
        Obsoletes = 34
    }
}
