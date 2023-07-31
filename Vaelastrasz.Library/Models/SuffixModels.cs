using System.Collections.Generic;

namespace Vaelastrasz.Library.Models
{
    public class CreateSuffixModel
    {
        public CreateSuffixModel()
        {
            Placeholders = new Dictionary<string, string>();
        }

        private Dictionary<string, string> Placeholders;
    }
}