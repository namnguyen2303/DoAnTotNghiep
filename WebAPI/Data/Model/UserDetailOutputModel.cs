using System;
using Data.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class UserDetailOutputModel
    {
        public int UserID { get; set; }
        public int? Role { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public DateTime? CreateDate { set; get; }
        public string Email { get; set; }
        public string LastRefCode { get; set; }
        public string KeyChat { get; set; }
        public string Token { get; set; }
        public string CraeteDateStr
        {
            set { }
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString(SystemParam.CONVERT_DATETIME) : "";
            }
        }
    }
}
