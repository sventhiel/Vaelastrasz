using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Models
{
    public class CreateDOIModel
    {
    }

    public class ReadDOIModel
    {
        public string Prefix => DOI.Split('/')[0];
        public string Suffix => DOI.Split('/')[1];
        public string DOI { get; set; }
    }
}
