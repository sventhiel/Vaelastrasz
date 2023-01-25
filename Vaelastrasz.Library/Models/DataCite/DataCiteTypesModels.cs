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

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteResourceType
    {
        [JsonPropertyName("Audiovisual")]
        Audiovisual = 1,

        [JsonPropertyName("Book")]
        Book = 2,

        [JsonPropertyName("BookChapter")]
        BookChapter = 3,

        [JsonPropertyName("Collection")]
        Collection = 4,

        [JsonPropertyName("ComputationalNotebook")]
        ComputationalNotebook = 5,

        [JsonPropertyName("ConferencePaper")]
        ConferencePaper = 6,

        [JsonPropertyName("ConferenceProceeding")]
        ConferenceProceeding = 7,

        [JsonPropertyName("DataPaper")]
        DataPaper = 8,

        [JsonPropertyName("Dataset")]
        Dataset = 9,

        [JsonPropertyName("Dissertation")]
        Dissertation = 10,

        [JsonPropertyName("Event")]
        Event = 11,

        [JsonPropertyName("Image")]
        Image = 12,

        [JsonPropertyName("InteractiveResource")]
        InteractiveResource = 13,

        [JsonPropertyName("JournalArticle")]
        JournalArticle = 14,

        [JsonPropertyName("Model")]
        Model = 15,

        [JsonPropertyName("OutputManagementPlan")]
        OutputManagementPlan = 16,

        [JsonPropertyName("PeerReview")]
        PeerReview = 17,

        [JsonPropertyName("PhysicalObject")]
        PhysicalObject = 18,

        [JsonPropertyName("Preprint")]
        Preprint = 19,

        [JsonPropertyName("Report")]
        Report = 20,

        [JsonPropertyName("Service")]
        Service = 21,

        [JsonPropertyName("Software")]
        Software = 22,

        [JsonPropertyName("Sound")]
        Sound = 23,

        [JsonPropertyName("Standard")]
        Standard = 24,

        [JsonPropertyName("Text")]
        Text = 25,

        [JsonPropertyName("Workflow")]
        Workflow = 26,

        [JsonPropertyName("Other")]
        Other = 27
    }
}