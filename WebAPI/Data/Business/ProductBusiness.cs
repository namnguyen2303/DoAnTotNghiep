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
    public class ProductBusiness : GenericBusiness
    {
        public ProductBusiness(TranDungShopEntities context = null) : base()
        {
        }

        public List<CategoryModel> ListCategory()
        {
            try
            {
                List<CategoryModel> listCategory = new List<CategoryModel>();
                var query = from c in cnn.product_categories
                            where c.is_active == SystemParam.ACTIVE
                            select new CategoryModel
                            {
                                CategoryID = c.id,
                                Name = c.name_product_category
                            };

                if (query != null && query.Count() > 0)
                {
                    listCategory = query.ToList();
                    return listCategory;
                }
                else
                    return listCategory;
            }
            catch (Exception)
            {
                return new List<CategoryModel>();
            }
        }

        public List<ListProductOutputModel> Search(string FromDate, string ToDate, string Name, int Category_id)
        {
            try
            {
                DateTime? fd = Util.ConvertDate(FromDate);
                DateTime? td = Util.ConvertDate(ToDate);

                List<ListProductOutputModel> list = (from p in cnn.products
                                                     where p.is_active.Equals(SystemParam.ACTIVE)
                                                     && (!String.IsNullOrEmpty(Name) ? p.product_name.ToLower().Contains(Name.ToLower()) : true)
                                                     && (Category_id != 0 ? p.product_category_id.Equals(Category_id) : true)
                                                     && (fd.HasValue ? p.created_at >= fd.Value : true)
                                                     && (td.HasValue ? p.created_at <= td.Value : true)
                                                     orderby p.id descending
                                                     select new ListProductOutputModel
                                                     {
                                                         ID = p.id,
                                                         Name = p.product_name,
                                                         ImgUrl = p.image_url,
                                                         Category_ID = p.product_category_id,
                                                         Category_Name = p.product_categories.name_product_category,
                                                         Price = p.price_start,
                                                         CreateDate = p.created_at
                                                     }).ToList();

                return list;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }

        public int CreateProduct(int CategoryID, string Code, string Name, string Price,string PriceSale, string ImageUrl, string Note, string Description, int New, int Sale, int Hot)
        {
            try
            {
                product item = new product();
                item.product_category_id = CategoryID;
                item.code = Code;
                item.product_name = Name;
                item.price_start = Convert.ToInt32((Price).ToString().Replace(",", ""));
                item.price_sale = Convert.ToInt32((PriceSale).ToString().Replace(",", ""));
                item.image_url = ImageUrl;
                item.created_at = DateTime.Now;
                item.description = Description;
                item.detail = Note;
                item.is_active = SystemParam.ACTIVE;
                item.is_new = New;
                item.is_hot = Hot;
                item.is_sale = Sale;
                cnn.products.Add(item);
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        public int DeleteProduct(int ID)
        {
            try
            {
                var obj = cnn.products.Find(ID);
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

        public ListProductOutputModel EditProduct(int ID)
        {
            try
            {
                var obj = cnn.products.Find(ID);
                ListProductOutputModel Item = new ListProductOutputModel();
                Item.ID = obj.id;
                Item.Code = obj.code;
                Item.Name = obj.product_name;
                Item.Category_ID = obj.product_category_id;
                Item.Price = obj.price_start;
                Item.PriceSale = obj.price_sale;
                Item.ImgUrl = obj.image_url;
                Item.Description = obj.description;
                Item.Content = obj.detail;
                Item.Is_New = obj.is_new;
                Item.Is_Hot = obj.is_hot;
                Item.Is_Sale = obj.is_sale;
                return Item;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ListProductOutputModel();
            }
        }

        public int SaveEditItem(int ID, string Code, string Name, int CategoryID, string ImageUrl, string Note, string Price, string PriceSale, string Description, int New, int Sale, int Hot)
        {
            try
            {
                product item = cnn.products.Find(ID);
                item.product_name = Name;
                item.code = Code;
                item.product_category_id = CategoryID;
                item.price_start = Convert.ToInt32((Price).ToString().Replace(",", ""));
                item.price_sale = Convert.ToInt32((PriceSale).ToString().Replace(",", ""));
                item.image_url = ImageUrl;
                item.description = Description;
                item.detail = Note;
                item.is_new = New;
                item.is_hot = Hot;
                item.is_sale = Sale;
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        public List<ListProductOutputModel> GetListProMen()
        {
            try
            {
                List<ListProductOutputModel> listProMen = cnn.products.Where(u => u.is_active.Equals(SystemParam.ACTIVE)
                                                          && (u.product_category_id == 4))
                .Select(u => new ListProductOutputModel
                {
                    ID = u.id,
                    ImgUrl = u.image_url,
                    Content = u.detail,
                    Description = u.description,
                    Category_ID = u.product_category_id,
                    Price = u.price_start,
                    PriceSale = u.price_sale,
                    Name = u.product_name,
                    Is_Hot = u.is_hot,
                    Category_Name = u.product_categories.name_product_category

                }).Take(8).ToList();
                return listProMen;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }
        public List<ListProductOutputModel> GetListProWoMen()
        {
            try
            {
                List<ListProductOutputModel> listProWMen = cnn.products.Where(u => u.is_active.Equals(SystemParam.ACTIVE)
                && (u.product_category_id == 3))
                .Select(u => new ListProductOutputModel
                {
                    ID = u.id,
                    ImgUrl = u.image_url,
                    Content = u.detail,
                    Description = u.description,
                    Price = u.price_start,
                    PriceSale = u.price_sale,
                    Name = u.product_name,
                    Category_ID = u.product_category_id,
                    Is_Hot = u.is_hot,
                    Category_Name = u.product_categories.name_product_category
                }).Take(8).ToList(); 
                return listProWMen;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }
        public List<ListProductOutputModel> GetListProKid()
        {
            try
            {
                List<ListProductOutputModel> listProKid = (from u in cnn.products
                                                           where u.is_active.Equals(SystemParam.ACTIVE)
                                                           && (u.product_category_id == 5)
                                                           select new ListProductOutputModel
                                                           {
                                                               ID = u.id,
                                                               ImgUrl = u.image_url,
                                                               Content = u.detail,
                                                               Description = u.description,
                                                               Category_ID = u.product_category_id,
                                                               Price = u.price_start,
                                                               PriceSale = u.price_sale,
                                                               Name = u.product_name,
                                                               Is_Hot = u.is_hot,
                                                               Category_Name = u.product_categories.name_product_category
                                                           }).Take(8).ToList();
                return listProKid;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }

        public List<ListProductOutputModel> GetListProSale()
        {
            try
            {
                List<ListProductOutputModel> list = (from p in cnn.products
                                                     where p.is_active.Equals(SystemParam.ACTIVE) && p.is_sale == 1
                                                     orderby p.id descending
                                                     select new ListProductOutputModel
                                                     {
                                                         ID = p.id,
                                                         Name = p.product_name,
                                                         //Code = p.code,
                                                         ImgUrl = p.image_url,
                                                         Category_ID = p.product_category_id,
                                                         Category_Name = p.product_categories.name_product_category,
                                                         Price = p.price_start,
                                                         PriceSale = p.price_sale,
                                                         CreateDate = p.created_at
                                                     }).ToList();

                return list;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }

        public List<ListProductOutputModel> GetListProNew()
        {
            try
            {
                List<ListProductOutputModel> list = (from p in cnn.products
                                                     where p.is_active.Equals(SystemParam.ACTIVE) && p.is_new == 1 /*&& p.category_product_id == 1*/
                                                     orderby p.id descending
                                                     select new ListProductOutputModel
                                                     {
                                                         ID = p.id,
                                                         Name = p.product_name,
                                                         ImgUrl = p.image_url,
                                                         Category_ID = p.product_category_id,
                                                         Category_Name = p.product_categories.name_product_category,
                                                         Price = p.price_start,
                                                         PriceSale = p.price_sale,
                                                         CreateDate = p.created_at
                                                     }).ToList();

                return list;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }
        public int GetListPro()
        {
            try
            {
                List<ListProductOutputModel> list = (from p in cnn.products
                                                     where p.is_active.Equals(SystemParam.ACTIVE) 
                                                     orderby p.id descending
                                                     select new ListProductOutputModel
                                                     {
                                                         ID = p.id,
                                                         Name = p.product_name,
                                                         ImgUrl = p.image_url,
                                                         Category_ID = p.product_category_id,
                                                         Category_Name = p.product_categories.name_product_category,
                                                         Price = p.price_start,
                                                         PriceSale = p.price_sale,
                                                         CreateDate = p.created_at
                                                     }).ToList();

                return list.Count();
            }
            catch
            {
                return 0;
            }
        }
        public List<ListProductOutputModel> GetListProHot()
        {
            try
            {
                List<ListProductOutputModel> list = (from p in cnn.products
                                                     where p.is_active.Equals(SystemParam.ACTIVE) && p.is_hot == 1 /*&& p.category_product_id == 1*/
                                                     orderby p.id descending
                                                     select new ListProductOutputModel
                                                     {
                                                         ID = p.id,
                                                         Name = p.product_name,
                                                         ImgUrl = p.image_url,
                                                         Category_ID = p.product_category_id,
                                                         Category_Name = p.product_categories.name_product_category,
                                                         Price = p.price_start,
                                                         PriceSale = p.price_sale,
                                                         CreateDate = p.created_at
                                                     }).ToList();

                return list;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }
        public List<ListProductOutputModel> getSpHot()
        {
            try
            {
                List<ListProductOutputModel> list = (from p in cnn.products
                                                     where p.is_active.Equals(SystemParam.ACTIVE) && p.is_hot == 1 /*&& p.category_product_id == 1*/
                                                     orderby p.created_at descending
                                                     select new ListProductOutputModel
                                                     {
                                                         ID = p.id,
                                                         Name = p.product_name,
                                                         ImgUrl = p.image_url,
                                                         Description = p.description,
                                                         Category_ID = p.product_category_id,
                                                         Category_Name = p.product_categories.name_product_category,
                                                         Price = p.price_start,
                                                         PriceSale = p.price_sale,

                                                         CreateDate = p.created_at
                                                     }).Take(4).ToList();

                return list;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }

        public ListProductOutputModel getSpNamHot()
        {
            try
            {
                ListProductOutputModel SpNamHot = (from u in cnn.products
                                                   where u.is_active.Equals(SystemParam.ACTIVE)
                                                   && u.is_hot == 1
                                                   && u.product_category_id == 4
                                                   orderby u.created_at descending
                                                   select new ListProductOutputModel
                                                   {
                                                       ID = u.id,
                                                       Name = u.product_name,
                                                       ImgUrl = u.image_url,
                                                       Description = u.description,
                                                       Category_ID = u.product_category_id,
                                                       Category_Name = u.product_categories.name_product_category,
                                                       Price = u.price_start,
                                                       PriceSale = u.price_sale,

                                                       CreateDate = u.created_at
                                                   }).FirstOrDefault();
                return SpNamHot;
            }
            catch
            {
                return new ListProductOutputModel();
            }
        }

        public ListProductOutputModel ProductDetail(int ID)
        {
            try
            {
                var prodetail = cnn.products.Find(ID);
                ListProductOutputModel p = new ListProductOutputModel();
                p.ID = prodetail.id;
                p.Name = prodetail.product_name;
                p.Category_Name = prodetail.product_categories.name_product_category;
                //p.Code = list.code;
                p.Category_ID = prodetail.product_category_id;
                p.Content = prodetail.detail;
                p.Description = prodetail.description;
                p.Price = prodetail.price_start;
                p.PriceSale = prodetail.price_sale;
                p.ImgUrl = prodetail.image_url;
                return p;
            }
            catch
            {
                return new ListProductOutputModel();
            }
        }



        public List<ListProductOutputModel> ProductRelated(int Category)
        {
            try
            {
                List<ListProductOutputModel> list = (from p in cnn.products
                                                     where p.is_active.Equals(SystemParam.ACTIVE) && p.product_category_id == Category
                                                     orderby p.id descending
                                                     select new ListProductOutputModel
                                                     {
                                                         ID = p.id,
                                                         Name = p.product_name,
                                                         //Code = p.code,
                                                         ImgUrl = p.image_url,
                                                         Category_ID = p.product_category_id,
                                                         Category_Name = p.product_categories.name_product_category,
                                                         Price = p.price_start,
                                                         PriceSale = p.price_sale,

                                                         CreateDate = p.created_at
                                                     }).ToList();

                return list;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }

        public List<ListProductOutputModel> ListProductCategory(int? Category, int? show)
        {
            try
            {
                List<ListProductOutputModel> list = (from p in cnn.products
                                                     where p.is_active.Equals(SystemParam.ACTIVE)
                                                     && (Category.HasValue ? p.product_category_id == Category : true)
                                                     select new ListProductOutputModel
                                                     {
                                                         ID = p.id,
                                                         Name = p.product_name,
                                                         //Code = p.code,
                                                         ImgUrl = p.image_url,
                                                         Category_ID = p.product_category_id,
                                                         Category_Name = p.product_categories.name_product_category,
                                                         Price = p.price_start,
                                                         PriceSale = p.price_sale,

                                                         CreateDate = p.created_at
                                                     }).ToList();
                if (show == -1)
                {
                    list.OrderByDescending(p => p.Price);
                }
                else if (show == 1)
                {
                    list.OrderBy(p => p.Price);
                }
                else
                {
                    list.OrderByDescending(p => p.CreateDate);
                }
                return list;
            }
            catch
            {
                return new List<ListProductOutputModel>();
            }
        }

        //public List<ListProductOutputModel> ListProductSearch(string q)
        //{
        //    try
        //    {
        //        List<ListProductOutputModel> list = (from p in cnn.products
        //                                             where p.is_active.Equals(SystemParam.ACTIVE)
        //                                             && p.name.ToLower().Contains(q.ToLower())
        //                                             select new ListProductOutputModel
        //                                             {
        //                                                 ID = p.id,
        //                                                 Name = p.name,
        //                                                 Code = p.code,
        //                                                 ImgUrl = p.product_image,
        //                                                 Category_ID = p.category_product_id,
        //                                                 Category_Name = p.product_category.name,
        //                                                 Price = p.price,
        //                                                  PriceSale = p.price_sale,

        //                                                 CreateDate = p.created_at
        //                                             }).ToList();
        //        return list;
        //    }
        //    catch
        //    {
        //        return new List<ListProductOutputModel>();
        //    }
        //}

    }
}
