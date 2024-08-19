using Newtonsoft.Json;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedItemTitle
    {
        public DataCiteRelatedItemTitle()
        { }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("titleType")]
        public DataCiteTitleType? TitleType { get; set; }
    }
}