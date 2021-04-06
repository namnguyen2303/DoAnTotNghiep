using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Utils;

namespace Data.Model
{
    public class ListCustomerOutputModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string PhoneNumber { set; get; }
        public int IsActive { set; get; }
        public int? Role { get; set; }
        public string Email { set; get; }
        public string Address { set; get; }
        public DateTime? CreateDate { set; get; }
        public string CreateDateStr
        {
            set { }
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString(SystemParam.CONVERT_DATETIME) : "";
            }
        }
        public int Gender { set; get; }
    }
}
