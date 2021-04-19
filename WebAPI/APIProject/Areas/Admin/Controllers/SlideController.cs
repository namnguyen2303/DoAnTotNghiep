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
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index()
        {
            return View();
        }

        [UserAuthenticationFilter]
        public PartialViewResult Search(int Page, string FromDate, string ToDate)
        {
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            IPagedList<ListSlideOutputModel> list = slideBusiness.Search(FromDate, ToDate).ToPagedList(Page, SystemParam.MAX_ROW_IN_LIST_WEB);
            return PartialView("_ListSlide", list);
        }

        [UserAuthenticationFilter]
        [ValidateInput(false)]
        public int CreateSlide(string ImageUrl)
        {
            try
            {
                return slideBusiness.CreateSlide(ImageUrl);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public PartialViewResult EditSlide(int ID)
        {
            try
            {
                ListSlideOutputModel slide = slideBusiness.EditSlide(ID);
                return PartialView("_EditSlide", slide);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return PartialView("_EditSlide", new ListSlideOutputModel());
            }
        }

        [UserAuthenticationFilter]
        [ValidateInput(false)]
        // Lưu lại cập nhật sản phẩm
        public int SaveEditSlide(int ID, string ImageUrl)
        {
            try
            {
                return slideBusiness.SaveEditSlide(ID, ImageUrl);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public int DeleteSlide(int ID)
        {
            try
            {
                return slideBusiness.DeleteSlide(ID);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }
    }
}