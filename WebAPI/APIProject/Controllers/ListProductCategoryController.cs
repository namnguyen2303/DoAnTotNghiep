using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Business;
using Data.Utils;
using APIProject.App_Start;
using Newtonsoft.Json;
using PagedList;


namespace APIProject.Controllers
{
    public class ListProductCategoryController : BaseController
    {
        // GET: ListProductCategory
        public ActionResult Index(int Page, int? Category, int? show)
        {
            ViewBag.Category = Category;
            IPagedList<ListProductOutputModel> list = productBusiness.ListProductCategory(Category, show).ToPagedList(Page, 3);
            ViewBag.getAllCate = categoryBusiness.getAllCate();
            return View(list);
        }
        //public ActionResult Index( )
        //{
        //    return View();
        //}
        public ActionResult Search(string q)
        {
            //List<ListProductOutputModel> list = productBusiness.ListProductSearch(q);
            return View();
        }
    }
}