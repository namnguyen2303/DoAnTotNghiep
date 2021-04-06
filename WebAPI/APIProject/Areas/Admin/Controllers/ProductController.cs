using System;
using APIProject.App_Start;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Model;
using PagedList;
using System.Web.Mvc;
using Data.Utils;
using APIProject.Controllers;

namespace APIProject.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        [UserAuthenticationFilter]
        public ActionResult Index()
        {
            ViewBag.Category = productBusiness.ListCategory();
            return View();
        }

        [UserAuthenticationFilter]
        public PartialViewResult Search(int Page, string FromDate, string ToDate, string Name, int Category_id)
        {
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            ViewBag.Name = Name;
            ViewBag.Category = Category_id;
            IPagedList<ListProductOutputModel> list = productBusiness.Search(FromDate, ToDate, Name, Category_id).ToPagedList(Page, SystemParam.MAX_ROW_IN_LIST_WEB);
            return PartialView("_ListProduct", list);
        }

        [UserAuthenticationFilter]
        public PartialViewResult AddProduct()
        {
            ViewBag.Category = productBusiness.ListCategory();
            return PartialView("_CreateProduct");
        }

        [UserAuthenticationFilter]
        [ValidateInput(false)]
        public int CreateProduct(int CategoryID, /*string Code,*/ string Name, string Price, string ImageUrl, string Note, string Description, int New, int Sale, int Hot)
        {
            try
            {
                return productBusiness.CreateProduct(CategoryID, /*Code,*/ Name, Price, ImageUrl, Note, Description, New, Sale, Hot);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public PartialViewResult EditProduct(int ID)
        {
            ViewBag.Category = productBusiness.ListCategory();
            var product = productBusiness.EditProduct(ID);
            return PartialView("_EditProduct", product);
        }

        [UserAuthenticationFilter]
        [ValidateInput(false)]
        // Lưu lại cập nhật sản phẩm
        public int SaveEditItem(int ID, /*string Code,*/ string Name, int CategoryID, string ImageUrl, string Note, string Price, string Description, int New, int Sale, int Hot)
        {
            try
            {
                return productBusiness.SaveEditItem(ID, /*Code,*/ Name, CategoryID, ImageUrl, Note, Price, Description, New, Sale, Hot);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public int DeleteProduct(int ID)
        {
            try
            {
                return productBusiness.DeleteProduct(ID);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }
    }
}