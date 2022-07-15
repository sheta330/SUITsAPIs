using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Models.Core_Models
{
    public class sub_category_prodacts
    {
        public int sub_category_prodactsid { get; set; }
        public int sub_categoryId { get; set; }
        public int Proudectid { get; set; }
        [ForeignKey("sub_categoryId")]
        public virtual sub_category sub_category { get; set; }
        [ForeignKey("Proudectid")]
        public virtual Proudect Proudect { get; set; }
    }
}
