using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Utils;

namespace Data.Model
{
    public class ListNewsWebOutputModel
    {
        public int ID { get; set; }
        public int? New { get; set; }
        public int? Hot { get; set; }
        public int CreateUserID { get; set; }
        public int Display { get; set; }
        public string CreateUserName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Depcription { get; set; }
        public string Content { get; set; }
        public string UrlImage { get; set; }
        public int Status { get; set; }
        public Nullable<int> Type { get; set; }
        public int TypeSend { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateDateStr
        {
            set { }
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString(SystemParam.CONVERT_DATETIME) : "";
            }
        }

        public string CreateDateHours
        {
            set { }
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString(SystemParam.CONVERT_DATETIME_HAVE_HOUR) : "";
            }
        }

        public string ImageStr
        {
            get
            {
                var img = UrlImage.Split(',');
                return img[0];
            }
        }
    }
}
