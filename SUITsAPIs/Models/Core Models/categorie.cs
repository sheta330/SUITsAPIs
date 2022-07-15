using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Models.Core_Models
{
    public class categorie
    {
        public int categorieId { get; set; }
        [Required, MaxLength(100)]
        public string categorieName { get; set; }
        public string categorieSlug { get; set; }
        public string imagepath { get; set; }
        public DateTime categorieCreatedate { get; set; }
    }
}
