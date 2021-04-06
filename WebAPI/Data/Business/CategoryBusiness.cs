using Data.DB;
using Data.Model;
using Data.Model.APIWeb;
using Data.Utils;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Business
{
    public class CategoryBusiness : GenericBusiness
    {
        public CategoryBusiness(TranDungShopEntities context = null) : base()
        {
        }
        
        public List<ListCategoryOutputModel> SearchProduct(string Name, string FromDate, string ToDate)
        {
            try
            {
                DateTime? fd = Util.ConvertDate(FromDate);
                DateTime? td = Util.ConvertDate(ToDate);

                List<ListCategoryOutputModel> list = (from c in cnn.product_categories
                                                      where c.is_active.Equals(SystemParam.ACTIVE)
                                                      && (!String.IsNullOrEmpty(Name) ? c.name_product_category.ToLower().Contains(Name.ToLower()) : true)
                                                      && (fd.HasValue ? c.created_at >= fd.Value : true)
                                                      && (td.HasValue ? c.created_at <= td.Value : true)
                                                      orderby c.id descending
                                                      select new ListCategoryOutputModel
                                                      {
                                                          ID = c.id,
                                                          Name = c.name_product_category,
                                                          CreateDate = c.created_at
                                                      }).ToList();

                return list;
            }
            catch
            {
                return new List<ListCategoryOutputModel>();
            }
        }

        //public List<ListCategoryPostOutputModel> SearchPost(string Name, string FromDate, string ToDate)
        //{
        //    try
        //    {
        //        DateTime? fd = Util.ConvertDate(FromDate);
        //        DateTime? td = Util.ConvertDate(ToDate);

        //        List<ListCategoryPostOutputModel> list = (from c in cnn.post_category
        //                                              where c.is_active == SystemParam.ACTIVE
        //                                              && (!String.IsNullOrEmpty(Name) ? c.name.ToLower().Contains(Name.ToLower()) : true)
        //                                              && (fd.HasValue ? c.created_at >= fd.Value : true)
        //                                              && (td.HasValue ? c.created_at <= td.Value : true)
        //                                              orderby c.id descending
        //                                              select new ListCategoryPostOutputModel
        //                                              {
        //                                                  ID = c.id,
        //                                                  Name = c.name,
        //                                                  Status = c.status,
        //                                                  CreateDate = c.created_at
        //                                              }).ToList();

        //        return list;
        //    }
        //    catch(Exception ex)
        //    {
        //        ex.ToString();
        //        return new List<ListCategoryPostOutputModel>();
        //    }
        //}

        //public Boolean CheckDuplicateCategory(string CategoryName)
        //{
        //    try
        //    {
        //        var cate = cnn.product_category.Where(u => u.name == CategoryName && u.is_active == SystemParam.ACTIVE).ToList();
        //        if (cate != null && cate.Count() > 0)
        //        {
        //            return SystemParam.BOOLEAN_TRUE;
        //        }
        //        return SystemParam.BOOLEAN_FALSE;
        //    }
        //    catch (Exception e)
        //    {
        //        e.ToString();
        //        return SystemParam.BOOLEAN_FALSE;
        //    }
        //}

        //public Boolean CheckDuplicatePostCategory(string CategoryName)
        //{
        //    try
        //    {
        //        var cate = cnn.post_category.Where(u => u.name == CategoryName && u.is_active == SystemParam.ACTIVE).ToList();
        //        if (cate != null && cate.Count() > 0)
        //        {
        //            return SystemParam.BOOLEAN_TRUE;
        //        }
        //        return SystemParam.BOOLEAN_FALSE;
        //    }
        //    catch (Exception e)
        //    {
        //        e.ToString();
        //        return SystemParam.BOOLEAN_FALSE;
        //    }
        //}

        public int CreateCategoryProduct(string Name)
        {
            try
            {
                //if (CheckDuplicateCategory(Name))
                //{
                //    return SystemParam.DUPLICATE_NAME;
                //}
                var q = cnn.product_categories.Select(u => u);
                product_categories pro = new product_categories();
                //pro.status = SystemParam.ACTIVE;
                pro.name_product_category = Name;
                pro.category_id = 1;
                pro.id = q.Count();
                pro.created_at = DateTime.Now;
                pro.is_active = SystemParam.ACTIVE;
                cnn.product_categories.Add(pro);
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        //public int CreateCategoryPost(string Name)
        //{
        //    try
        //    {
        //        if (CheckDuplicatePostCategory(Name))
        //        {
        //            return SystemParam.DUPLICATE_NAME;
        //        }
        //        post_category pro = new post_category();
        //        pro.is_active = SystemParam.ACTIVE;
        //        pro.status = SystemParam.ACTIVE;
        //        pro.name = Name;
        //        pro.created_at = DateTime.Now;
        //        cnn.post_category.Add(pro);
        //        cnn.SaveChanges();
        //        return SystemParam.RETURN_TRUE;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return SystemParam.RETURN_FALSE;
        //    }
        //}

        public int DeleteCategoryProduct(int ID)
        {
            try
            {
                var p = cnn.product_categories.Find(ID);
                p.is_active = SystemParam.ACTIVE_FALSE;
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        //public int DeleteCategoryPost(int ID)
        //{
        //    try
        //    {
        //        var p = cnn.post_category.Find(ID);
        //        p.is_active = SystemParam.ACTIVE_FALSE;
        //        cnn.SaveChanges();
        //        return SystemParam.RETURN_TRUE;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return SystemParam.RETURN_FALSE;
        //    }
        //}

        public ListCategoryOutputModel EditCategoryProduct(int ID)
        {
            try
            {
                ListCategoryOutputModel categoryDetail = new ListCategoryOutputModel();
                var query = (from c in cnn.product_categories
                             where c.is_active == SystemParam.ACTIVE
                             && c.id == ID
                             select new ListCategoryOutputModel
                             {
                                 ID = c.id,
                                 Name = c.name_product_category,
                             }).FirstOrDefault();
                if (query != null && query.ID > 0)
                {
                    return categoryDetail = query;
                }
                else
                {
                    return categoryDetail;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ListCategoryOutputModel();
            }
        }

        public int SaveEditCategoryProduct(int ID, string Name, int Status)
        {
            try
            {
                var c = cnn.product_categories.Find(ID);
                c.name_product_category = Name;
                //c.updated_at = DateTime.Now;
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }
        public List<ListCategoryOutputModel> getAllCate()
        {
            try
            {
                List<ListCategoryOutputModel> listCate = cnn.product_categories.Where(u => u.is_active.Equals(SystemParam.ACTIVE)).
                                                        Select(u => new ListCategoryOutputModel
                                                        {
                                                            ID = u.id,
                                                            Name = u.name_product_category
                                                        }).ToList();
                return listCate;
            }
            catch
            {
                return new List<ListCategoryOutputModel>();
            }
        }
        public List<ListCategoryOutputModel> getCategories0()
        {
            try
            {
                List<ListCategoryOutputModel> listCate = cnn.product_categories.Where(u => u.category_id == 0).
                                                        Select(u => new ListCategoryOutputModel
                                                        {
                                                            ID = u.id,
                                                            Name = u.name_product_category
                                                        }).ToList();
                return listCate;
            }
            catch
            {
                return new List<ListCategoryOutputModel>();
            }
        }
        public List<ListCategoryOutputModel> getCategories1()
        {
            try
            {
                List<ListCategoryOutputModel> listCate = cnn.product_categories.Where(u => u.category_id == 1).
                                                        Select(u => new ListCategoryOutputModel
                                                        {
                                                            ID = u.id,
                                                            Name = u.name_product_category
                                                        }).ToList();
                return listCate;
            }
            catch
            {
                return new List<ListCategoryOutputModel>();
            }
        }
        public List<ListCategoryOutputModel> getCategories2()
        {
            try
            {
                List<ListCategoryOutputModel> listCate = cnn.product_categories.Where(u => u.category_id == 2).
                                                        Select(u => new ListCategoryOutputModel
                                                        {
                                                            ID = u.id,
                                                            Name = u.name_product_category
                                                        }).ToList();
                return listCate;
            }
            catch
            {
                return new List<ListCategoryOutputModel>();
            }
        }
        //public ListCategoryOutputModel EditCategoryPost(int ID)
        //{
        //    try
        //    {
        //        ListCategoryOutputModel categoryDetail = new ListCategoryOutputModel();
        //        var query = (from c in cnn.post_category
        //                     where c.is_active == SystemParam.ACTIVE
        //                     && c.id == ID
        //                     select new ListCategoryOutputModel
        //                     {
        //                         ID = c.id,
        //                         Name = c.name,
        //                         Status = c.status
        //                     }).FirstOrDefault();
        //        if (query != null && query.ID > 0)
        //        {
        //            return categoryDetail = query;
        //        }
        //        else
        //        {
        //            return categoryDetail;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return new ListCategoryOutputModel();
        //    }
        //}

        //public int SaveEditCategoryPost(int ID, string Name, int Status)
        //{
        //    try
        //    {
        //        var c = cnn.post_category.Find(ID);
        //        c.name = Name;
        //        c.status = Status;
        //        c.updated_at = DateTime.Now;
        //        cnn.SaveChanges();
        //        return SystemParam.RETURN_TRUE;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return SystemParam.RETURN_FALSE;
        //    }
        //}
    }
}
