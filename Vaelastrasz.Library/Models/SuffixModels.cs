using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Models
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
