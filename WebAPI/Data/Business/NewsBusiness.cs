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
    public class NewsBusiness : GenericBusiness
    {
        public NewsBusiness(TranDungShopEntities context = null) : base()
        {
        }
        //public List<CategoryModel> ListCategory()
        //{
        //    try
        //    {
        //        List<CategoryModel> listCategory = new List<CategoryModel>();
        //        var query = from c in cnn.post_category
        //                    where c.is_active == SystemParam.ACTIVE && c.status == SystemParam.ACTIVE
        //                    select new CategoryModel
        //                    {
        //                        CategoryID = c.id,
        //                        Name = c.name
        //                    };

        //        if (query != null && query.Count() > 0)
        //        {
        //            listCategory = query.ToList();
        //            return listCategory;
        //        }
        //        else
        //            return listCategory;
        //    }
        //    catch (Exception)
        //    {
        //        return new List<CategoryModel>();
        //    }
        //}

        public List<ListNewsWebOutputModel> Search(string FromDate, string ToDate, string Name/*, int Category_id*/)
        {
            try
            {
                DateTime? fd = Util.ConvertDate(FromDate);
                DateTime? td = Util.ConvertDate(ToDate);

                List<ListNewsWebOutputModel> list = (from p in cnn.news
                                                     where p.is_active.Equals(SystemParam.ACTIVE)
                                                     && (!String.IsNullOrEmpty(Name) ? p.summary.ToLower().Contains(Name.ToLower()) : true)
                                                     // && (Category_id != 0 ? p.post_category_id.Equals(Category_id) : true)
                                                     && (fd.HasValue ? p.created_at >= fd.Value : true)
                                                     && (td.HasValue ? p.created_at <= td.Value : true)
                                                     orderby p.id descending
                                                     select new ListNewsWebOutputModel
                                                     {
                                                         ID = p.id,
                                                         Title = p.summary,
                                                         Content = p.content,
                                                         UrlImage = p.imageUrl,
                                                         // CategoryID = p.post_category_id,
                                                         // CategoryName = p.post_category.name,
                                                         CreateDate = p.created_at
                                                     }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new List<ListNewsWebOutputModel>();
            }
        }

        public int CreatePost(/*int CategoryID,*/ string Name, string ImageUrl, string Note, string Description/*, int type*/)
        {
            try
            {
                news item = new news();
                item.summary = Name;
                item.imageUrl = ImageUrl;
                item.created_at = DateTime.Now;
                item.content = Description;
                //item.type_new = type;
                item.is_active = SystemParam.ACTIVE;
                cnn.news.Add(item);
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        public ListNewsWebOutputModel EditPost(int ID)
        {
            try
            {
                var obj = cnn.news.Find(ID);
                ListNewsWebOutputModel Item = new ListNewsWebOutputModel();
                Item.ID = obj.id;
                Item.Title = obj.summary;
                Item.UrlImage = obj.imageUrl;
                Item.Depcription = obj.summary;
                Item.Content = obj.content;
                Item.Type = obj.type_new;
                return Item;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ListNewsWebOutputModel();
            }
        }

        public int SaveEditPost(int ID, string Name, /*int CategoryID,*/ string ImageUrl, string Note, string Description/*, int type*/)
        {
            try
            {
                news item = cnn.news.Find(ID);
                item.summary = Name;
                item.imageUrl = ImageUrl;
                item.summary = Description;
                item.content = Note;
                //item.type_new = type;
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        public int DeletePost(int ID)
        {
            try
            {
                var obj = cnn.news.Find(ID);
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


        //public List<ListNewsWebOutputModel> ListPostNew()
        //{
        //    try
        //    {
        //        List<ListNewsWebOutputModel> list = (from p in cnn.news
        //                                             where p.is_active.Equals(SystemParam.ACTIVE) && p.is_new == 1
        //                                             orderby p.id descending
        //                                             select new ListNewsWebOutputModel
        //                                             {
        //                                                 ID = p.id,
        //                                                 Title = p.name,
        //                                                 UrlImage = p.post_image,
        //                                                 Content = p.content,
        //                                                 Depcription = p.description,
        //                                                 CreateDate = p.created_at
        //                                             }).ToList();

        //        return list;
        //    }
        //    catch
        //    {
        //        return new List<ListNewsWebOutputModel>();
        //    }
        //}

        //public List<ListNewsWebOutputModel> ListPostCategory(int? Category)
        //{
        //    try
        //    {
        //        List<ListNewsWebOutputModel> list = (from p in cnn.posts
        //                                             where p.is_active.Equals(SystemParam.ACTIVE)
        //                                             && (Category.HasValue ? p.post_category_id == Category : true)
        //                                             orderby p.created_at descending
        //                                             select new ListNewsWebOutputModel
        //                                             {
        //                                                 ID = p.id,
        //                                                 Title = p.name,
        //                                                 Content = p.content,
        //                                                 Depcription = p.description,
        //                                                 UrlImage = p.post_image,
        //                                                 CategoryID = p.post_category_id,
        //                                                 CategoryName = p.post_category.name,
        //                                                 CreateDate = p.created_at
        //                                             }).ToList();

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return new List<ListNewsWebOutputModel>();
        //    }
        //}

        public List<ListNewsWebOutputModel> GetListTopNews()
        {
            try
            {
                List<ListNewsWebOutputModel> list = (from p in cnn.news
                                                     where p.is_active.Equals(SystemParam.ACTIVE) && p.type_new == 1
                                                     orderby p.created_at descending
                                                     select new ListNewsWebOutputModel
                                                     {
                                                         ID = p.id,
                                                         Title = p.summary,
                                                         Content = p.content,
                                                         UrlImage = p.imageUrl,
                                                         CreateDate = p.created_at
                                                     }).Take(3).ToList();

                return list;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new List<ListNewsWebOutputModel>();
            }
        }

        //public List<ListNewsWebOutputModel> PostNew()
        //{
        //    try
        //    {
        //        List<ListNewsWebOutputModel> list = (from p in cnn.posts
        //                                             where p.is_active.Equals(SystemParam.ACTIVE) && p.is_new == 1
        //                                             orderby p.created_at descending
        //                                             select new ListNewsWebOutputModel
        //                                             {
        //                                                 ID = p.id,
        //                                                 Title = p.name,
        //                                                 Content = p.content,
        //                                                 Depcription = p.description,
        //                                                 UrlImage = p.post_image,
        //                                                 CategoryID = p.post_category_id,
        //                                                 CategoryName = p.post_category.name,
        //                                                 CreateDate = p.created_at
        //                                             }).Take(5).ToList();

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return new List<ListNewsWebOutputModel>();
        //    }
        //}

        public ListNewsWebOutputModel Newscontent(int ID)
        {
            try
            {
                var post = cnn.news.Find(ID);
                ListNewsWebOutputModel p = new ListNewsWebOutputModel();
                p.ID = post.id;
                p.Title = post.summary;
                p.Content = post.content;
                p.UrlImage = post.imageUrl;
                return p;
            }
            catch
            {
                return new ListNewsWebOutputModel();
            }
        }
    }
}
