using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Vaelastrasz.Library.Types
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteContributorType
    {
        [EnumMember(Value = "ContactPerson")]
        ContactPerson = 1,

        [EnumMember(Value = "DataCollector")]
        DataCollector = 2,

        [EnumMember(Value = "DataCurator")]
        DataCurator = 3,

        [EnumMember(Value = "DataManager")]
        DataManager = 4,

        [EnumMember(Value = "Distributor")]
        Distributor = 5,

        [EnumMember(Value = "Editor")]
        Editor = 6,

        [EnumMember(Value = "HostingInstitution")]
        HostingInstitution = 7,

        [EnumMember(Value = "Producer")]
        Producer = 8,

        [EnumMember(Value = "ProjectLeader")]
        ProjectLeader = 9,

        [EnumMember(Value = "ProjectManager")]
        ProjectManager = 10,

        [EnumMember(Value = "ProjectMember")]
        ProjectMember = 11,

        [EnumMember(Value = "RegistrationAgency")]
        RegistrationAgency = 12,

        [EnumMember(Value = "RegistrationAuthority")]
        RegistrationAuthority = 13,

        [EnumMember(Value = "RelatedPerson")]
        RelatedPerson = 14,

        [EnumMember(Value = "Researcher")]
        Researcher = 15,

        [EnumMember(Value = "ResearchGroup")]
        ResearchGroup = 16,

        [EnumMember(Value = "RightsHolder")]
        RightsHolder = 17,

        [EnumMember(Value = "Sponsor")]
        Sponsor = 18,

        [EnumMember(Value = "Supervisor")]
        Supervisor = 19,

        [EnumMember(Value = "WorkPackageLeader")]
        WorkPackageLeader = 20,

        [EnumMember(Value = "Other")]
        Other = 21,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteDateType
    {
        [EnumMember(Value = "Accepted")]
        Accepted = 1,

        [EnumMember(Value = "Available")]
        Available = 2,

        [EnumMember(Value = "Copyrighted")]
        Copyrighted = 3,

        [EnumMember(Value = "Collected")]
        Collected = 4,

        [EnumMember(Value = "Created")]
        Created = 5,

        [EnumMember(Value = "Issued")]
        Issued = 6,

        [EnumMember(Value = "Submitted")]
        Submitted = 7,

        [EnumMember(Value = "Updated")]
        Updated = 8,

        [EnumMember(Value = "Valid")]
        Valid = 9,

        [EnumMember(Value = "Withdrawn")]
        Withdrawn = 10,

        [EnumMember(Value = "Other")]
        Other = 11
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteDescriptionType
    {
        [EnumMember(Value = "Abstract")]
        Abstract = 1,

        [EnumMember(Value = "Methods")]
        Methods = 2,

        [EnumMember(Value = "SeriesInformation")]
        SeriesInformation = 3,

        [EnumMember(Value = "TableOfContents")]
        TableOfContents = 4,

        [EnumMember(Value = "TechnicalInfo")]
        TechnicalInfo = 5,

        [EnumMember(Value = "Other")]
        Other = 6
    }

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
    public enum DataCiteFunderIdentifierType
    {
        [EnumMember(Value = "Crossref Funder ID")]
        CrossrefFunderID = 1,

        [EnumMember(Value = "GRID")]
        GRID = 2,

        [EnumMember(Value = "ISNI")]
        ISNI = 3,

        [EnumMember(Value = "ROR")]
        ROR = 4,

        [EnumMember(Value = "Other")]
        Other = 5
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteNameType
    {
        [EnumMember(Value = "Personal")]
        Personal = 1,

        [EnumMember(Value = "Organizational")]
        Organizational = 2
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteRelatedIdentifierType
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
    public enum DataCiteRelationType
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
    public enum DataCiteTitleType
    {
        [EnumMember(Value = "AlternativeTitle")]
        AlternativeTitle = 1,

        [EnumMember(Value = "Subtitle")]
        Subtitle = 2,

        [EnumMember(Value = "TranslatedTitle")]
        TranslatedTitle = 3,

        [EnumMember(Value = "Other")]
        Other = 4
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteType
    {
        [EnumMember(Value = "dois")]
        DOIs = 1
    }
}