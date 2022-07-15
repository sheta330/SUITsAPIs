using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Models.Core_Models
{
    public class Discound
    {
        public int discoundid { get; set; }
        public int TheDiscound { get; set; }
        public string Discounddesc { get; set; }
        public int Proudectid { get; set; }
        [ForeignKey("Proudectid")]
        public Proudect Proudect { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime Delatedate { get; set; }
        public string imagepath { get; set; }

    }
}
