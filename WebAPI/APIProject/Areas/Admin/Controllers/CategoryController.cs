using APIProject.App_Start;
using Data.Model;
using Data.Utils;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIProject.Controllers;

namespace APIProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, member")]
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        [UserAuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }

        [UserAuthenticationFilter]
        public PartialViewResult SearchProduct(int Page, string Name, string FromDate, string ToDate)
        {
            ViewBag.Name = Name;
            ViewBag.FromDateCate = FromDate;
            ViewBag.ToDateCate = ToDate;
            IPagedList<ListCategoryOutputModel> list = categoryBusiness.SearchProduct(Name, FromDate, ToDate).ToPagedList(Page, SystemParam.MAX_ROW_IN_LIST_WEB);
            return PartialView("_CategoryProduct", list);
        }

        //[UserAuthenticationFilter]
        //public PartialViewResult SearchPost(int Page, string Name, string FromDate, string ToDate)
        //{
        //    ViewBag.Name = Name;
        //    ViewBag.FromDateCate = FromDate;
        //    ViewBag.ToDateCate = ToDate;
        //    IPagedList<ListCategoryPostOutputModel> list = categoryBusiness.SearchPost(Name, FromDate, ToDate).ToPagedList(Page, SystemParam.MAX_ROW_IN_LIST_WEB);
        //    return PartialView("_CategoryPost", list);
        //}

        [UserAuthenticationFilter]
        public int CreateCategoryProduct(string Name)
        {
            try
            {
                return categoryBusiness.CreateCategoryProduct(Name);
            }
            catch
            {
                return SystemParam.RETURN_FALSE;
            }
        }

        //[UserAuthenticationFilter]
        //public int CreateCategoryPost(string Name)
        //{
        //    try
        //    {
        //        return categoryBusiness.CreateCategoryPost(Name);
        //    }
        //    catch
        //    {
        //        return SystemParam.RETURN_FALSE;
        //    }
        //}

        [UserAuthenticationFilter]
        public int DeleteCategoryProduct(int ID)
        {
            try
            {
                return categoryBusiness.DeleteCategoryProduct(ID);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        //[UserAuthenticationFilter]
        //public int DeleteCategoryPost(int ID)
        //{
        //    try
        //    {
        //        return categoryBusiness.DeleteCategoryPost(ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return SystemParam.RETURN_FALSE;
        //    }
        //}

        [UserAuthenticationFilter]
        public PartialViewResult EditCategoryProduct(int ID)
        {
            try
            {
                ListCategoryOutputModel CategoryDetail = categoryBusiness.EditCategoryProduct(ID);
                return PartialView("_EditCategoryProduct", CategoryDetail);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return PartialView("_EditCategoryProduct", new ListCategoryOutputModel());
            }
        }

        [UserAuthenticationFilter]
        public int SaveEditCategoryProduct(int ID, string Name, int Status)
        {
            try
            {
                return categoryBusiness.SaveEditCategoryProduct(ID, Name, Status);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }

        //[UserAuthenticationFilter]
        //public PartialViewResult EditCategoryPost(int ID)
        //{
        //    try
        //    {
        //        ListCategoryOutputModel CategoryDetail = categoryBusiness.EditCategoryPost(ID);
        //        return PartialView("_EditCategoryPost", CategoryDetail);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return PartialView("_EditCategoryPost", new ListCategoryOutputModel());
        //    }
        //}

        //[UserAuthenticationFilter]
        //public int SaveEditCategoryPost(int ID, string Name, int Status)
        //{
        //    try
        //    {
        //        return categoryBusiness.SaveEditCategoryPost(ID, Name, Status);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return SystemParam.RETURN_FALSE;
        //    }
        //}
    }
}