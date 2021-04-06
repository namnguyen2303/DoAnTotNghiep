using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Utils;

namespace Data.Model
{
    public class ListProductOutputModel
    {
        public int ID { set; get; }
        public string Code { set; get; }
        public int Category_ID { set; get; }
        public int? Is_New { set; get; }
        public int? Is_Hot { set; get; }
        public int? Is_Sale { set; get; }
        public string Name { set; get; }
        public string Category_Name { set; get; }
        //public float? Price { set; get; }
        public Nullable<double> Price { set; get; }
        public string ImgUrl { set; get; }
        public string Content { set; get; }
        public string Description { set; get; }
        public DateTime? CreateDate { set; get; }
        public string CreateDateStr
        {
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString(SystemParam.CONVERT_DATETIME) : "";
            }
        }

        public string ImageStr
        {
            get
            {
                var img = ImgUrl.Split(',');
                return img[0];
            }
        }

        public string[] ListImageStr
        {
            get
            {
                return ImgUrl.Split(',');
            }
        }
    }
}
