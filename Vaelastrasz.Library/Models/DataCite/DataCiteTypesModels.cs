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
        Audiovisual = 1,
        Book = 2,
        BookChapter = 3,
        Collection = 4,
        ComputationalNotebook = 5,
        ConferencePaper = 6,
        ConferenceProceeding = 7,
        DataPaper = 8,
        Dataset = 9,
        Dissertation = 10,
        Event = 11,
        Image = 12,
        InteractiveResource = 13,
        JournalArticle = 14,
        Model = 15,
        OutputManagementPlan = 16,
        PeerReview = 17,
        PhysicalObject = 18,
        Preprint = 19,
        Report = 20,
        Service = 21,
        Software = 22,
        Sound = 23,
        Standard = 24,
        Text = 25,
        Workflow = 26,
        Other = 27
    }
}