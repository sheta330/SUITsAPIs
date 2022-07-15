using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Models.Core_Models
{
    public class productimgs
    {
        public int id { get; set; }
        public string imgpath { get; set; }
        public int Proudectid { get; set; }
        [ForeignKey("Proudectid")]
        public Proudect Proudect { get; set; }
    }
}
