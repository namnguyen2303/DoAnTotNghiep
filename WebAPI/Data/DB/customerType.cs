using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DB
{
    public class customerType
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public virtual IEnumerable<customer> Customers { get; set; }
    }
}
