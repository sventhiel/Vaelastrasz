using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTypesModel
    {
        [JsonPropertyName("resourceTypeGeneral")]
        public DataCiteResourceType ResourceTypeGeneral { get; set; }

        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; }

        [JsonPropertyName("schemaOrg")]
        public string SchemaOrg { get; set; }

        [JsonPropertyName("bibtex")]
        public string Bibtex { get; set; }

        [JsonPropertyName("citeproc")]
        public string Citeproc { get; set; }

        [JsonPropertyName("ris")]
        public string Ris { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
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
}