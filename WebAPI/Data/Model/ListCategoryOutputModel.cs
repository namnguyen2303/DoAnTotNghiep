using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Utils;

namespace Data.Model
{
    public class ListCategoryOutputModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Status { get; set; }
        public string getStrStatus
        {
            get
            {
                if (Status == 0)
                {
                    return "Hoạt động";
                }
                else if (Status == 1)
                {
                    return "Ngừng hoạt động";
                }
                else
                {
                    return "";
                }
            }
        }
        public string CreateDateStr
        {
            set { }
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString(SystemParam.CONVERT_DATETIME) : "";
            }
        }
    }
}
