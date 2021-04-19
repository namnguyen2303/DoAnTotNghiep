using Data.DB;
using Data.Model;
using Data.Model.APIWeb;
using Data.Utils;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Business
{
    public class SlideBusiness : GenericBusiness
    {
        public SlideBusiness(TranDungShopEntities context = null) : base()
        {
        }

        public List<ListSlideOutputModel> Search(string FromDate, string ToDate)
        {
            try
            {
                DateTime? fd = Util.ConvertDate(FromDate);
                DateTime? td = Util.ConvertDate(ToDate);

                List<ListSlideOutputModel> list = (from p in cnn.slides
                                                   where p.is_active.Equals(SystemParam.ACTIVE)
                                                   && (fd.HasValue ? p.created_at >= fd.Value : true)
                                                   && (td.HasValue ? p.created_at <= td.Value : true)
                                                   orderby p.id descending
                                                   select new ListSlideOutputModel
                                                   {
                                                       ID = p.id,
                                                       ImgUrl = p.image_url,
                                                       CreateDate = p.created_at
                                                   }).ToList();

                return list;
            }
            catch
            {
                return new List<ListSlideOutputModel>();
            }
        }

        public int CreateSlide(string ImageUrl)
        {
            try
            {
                slide item = new slide();
                item.image_url = ImageUrl;
                item.created_at = DateTime.Now;
                item.is_active = SystemParam.ACTIVE;
                cnn.slides.Add(item);
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        public ListSlideOutputModel EditSlide(int ID)
        {
            try
            {
                ListSlideOutputModel slide = new ListSlideOutputModel();
                var query = (from c in cnn.slides
                             where c.is_active == SystemParam.ACTIVE
                             && c.id == ID
                             select new ListSlideOutputModel
                             {
                                 ID = c.id,
                                 ImgUrl = c.image_url
                             }).FirstOrDefault();
                if (query != null && query.ID > 0)
                {
                    return slide = query;
                }
                else
                {
                    return slide;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ListSlideOutputModel();
            }
        }

        public int SaveEditSlide(int ID, string ImageUrl)
        {
            try
            {
                slide item = cnn.slides.Find(ID);
                item.image_url = ImageUrl;
                //item.updated_at = DateTime.Now;
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        public int DeleteSlide(int ID)
        {
            try
            {
                var obj = cnn.slides.Find(ID);
                obj.is_active = SystemParam.ACTIVE_FALSE;
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        public List<ListSlideOutputModel> ListSlide()
        {
            try
            {
                List<ListSlideOutputModel> list = (from p in cnn.slides
                                                   where p.is_active.Equals(SystemParam.ACTIVE)
                                                   orderby p.id descending
                                                   select new ListSlideOutputModel
                                                   {
                                                       ID = p.id,
                                                       ImgUrl = p.image_url,
                                                       CreateDate = p.created_at
                                                   }).ToList();

                return list;
            }
            catch
            {
                return new List<ListSlideOutputModel>();
            }
        }
    }
}
