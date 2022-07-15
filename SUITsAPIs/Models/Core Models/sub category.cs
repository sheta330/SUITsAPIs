using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Models.Core_Models
{
    public class sub_category
    {
        public int id { get; set; }
        public string subcategoryname { get; set; }
        public string subcategoryslug { get; set; }
        public string imagepath { get; set; }
        public int categorieId { get; set; }
        [ForeignKey("categorieId")]
        public virtual categorie Categorie { get; set; }
    }
}
