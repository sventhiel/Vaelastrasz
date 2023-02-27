namespace Vaelastrasz.Server.Models
{
    public class CreateDOIModel
    {
        public Dictionary<string, string> Placeholders { get; set; }
    }

    public class ReadDOIModel
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
    }

    public class UpdateDOIModel
    {
    }
}