using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vaelastrasz.Library.Models
{
    public class ConceptModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required, Url]
        public string Url { get; set; }
        public List<ConceptItemModel> Items { get; set; }
    }

    public class ConceptItemModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required, Url]
        public string Url { get; set; }

        public bool IsOptional { get; set; }
        public bool IsComplex { get; set; }
        
        [Required]
        public string XPath { get; set; }

        public List<ConceptItemModel> Children { get; set; }
    }
}
