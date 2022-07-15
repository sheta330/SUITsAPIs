using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Models.Core_Models
{
    public class Proudect
    {
        public int Proudectid { get; set; }
        [Required, StringLength(100)]
        public string Proudectname { get; set; }
        public string ProudSlug { get; set; }
        [Required, StringLength(500)]
        public string Proudectdesc { get; set; }
        [Required]
        public decimal Proudectprice { get; set; }
        public string ProudectImage { get; set; }
        public int Sexid { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("Sexid")]
        public virtual Sex Sex { get; set; }
        public DateTime Createdate { get; set; }

    }
}
