using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Utils;

namespace Data.Model
{
    public class ListSlideOutputModel
    {
        public int ID { set; get; }
        public string ImgUrl { set; get; }
        public string summary { set; get; }
        public DateTime? CreateDate { set; get; }
        public string CreateDateStr
        {
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString(SystemParam.CONVERT_DATETIME) : "";
            }
        }
    }
}
