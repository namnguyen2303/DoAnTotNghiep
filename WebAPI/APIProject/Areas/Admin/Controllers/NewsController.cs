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
    [Authorize(Roles = "admin, member")]
    public class NewsController : BaseController
    {
        // GET: Admin/Post
        [UserAuthenticationFilter]
        public ActionResult Index()
        {
            //ViewBag.Category = newsBusiness.ListCategory();
            return View();
        }

        [UserAuthenticationFilter]
        public PartialViewResult AddPost()
        {
            //ViewBag.Category = newsBusiness.ListCategory();
            return PartialView("_CreatePost");
        }

        [UserAuthenticationFilter]
        public PartialViewResult EditPost(int ID)
        {
            //ViewBag.Category = newsBusiness.ListCategory();
            var post = newsBusiness.EditPost(ID);
            return PartialView("_EditPost", post);
        }

        [UserAuthenticationFilter]
        public PartialViewResult Search(int Page, string FromDate, string ToDate, string Name/*, int Category_id*/)
        {
            try
            {
                ViewBag.FromDateNews = FromDate;
                ViewBag.ToDateNews = ToDate;
                ViewBag.Name = Name;
                //ViewBag.Category = Category_id;
                IPagedList<ListNewsWebOutputModel> list = newsBusiness.Search(FromDate, ToDate, Name/*, Category_id*/).ToPagedList(Page, SystemParam.MAX_ROW_IN_LIST_WEB);
                return PartialView("_ListPost", list);
            }
            catch
            {
                return PartialView("_ListPost", new List<ListNewsWebOutputModel>().ToPagedList(1, 1));
            }
        }

        [UserAuthenticationFilter]
        [ValidateInput(false)]
        public int CreatePost(/*int CategoryID,*/ string Name, string ImageUrl, string Note, string Description/*, int type*/)
        {
            try
            {
                return newsBusiness.CreatePost(/*CategoryID,*/ Name, ImageUrl, Note, Description/*, type*/);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        [ValidateInput(false)]
        // Lưu lại cập nhật sản phẩm
        public int SaveEditPost(int ID, string Name, /*int CategoryID,*/ string ImageUrl, string Note, string Description/*, int type*/)
        {
            try
            {
                return newsBusiness.SaveEditPost(ID, Name,/* CategoryID,*/ ImageUrl, Note, Description/*, type*/);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public int DeletePost(int ID)
        {
            try
            {
                return newsBusiness.DeletePost(ID);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }
    }
}