using System.Collections.Generic;

namespace Vaelastrasz.Library.Models
{
    public class CreateDOIModel
    {
        public CreateDOIModel()
        {
            Placeholders = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Placeholders { get; set; }
    }

    public class ReadDOIModel
    {
        public string DOI { get; set; }
        public string Prefix => DOI.Split('/')[0];
        public string Suffix => DOI.Split('/')[1];
    }
}