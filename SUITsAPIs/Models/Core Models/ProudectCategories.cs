using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Models.Core_Models
{
    public class ProudectCategories
    {
        public int ProudectCategoriesid { get; set; }
        public int categorieId { get; set; }
        public int Proudectid { get; set; }
        [ForeignKey("categorieId")]
        public virtual categorie Categorie { get; set; }
        [ForeignKey("Proudectid")]
        public virtual Proudect Proudect { get; set; }
    }
}
