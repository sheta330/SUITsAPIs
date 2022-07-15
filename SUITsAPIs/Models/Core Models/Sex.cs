using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Models.Core_Models
{
    public class Sex
    {
        public int Sexid { get; set; }
        [Required, StringLength(100)]
        public string Sexname { get; set; }
    }
}
