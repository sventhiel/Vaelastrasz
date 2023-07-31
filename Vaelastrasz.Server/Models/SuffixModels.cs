namespace Vaelastrasz.Server.Models
{
    public class CreateSuffixModel
    {
        public Dictionary<string, string> Placeholders { get; set; }

        public CreateSuffixModel() 
        {
            Placeholders = new Dictionary<string, string>();
        }
    }
}
