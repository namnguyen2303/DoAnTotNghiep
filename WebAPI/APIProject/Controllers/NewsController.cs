using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIProject.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult Index(int ID)
        {
            ViewBag.NewsDetail = newsBusiness.NewsDetail(ID);
            ViewBag.listTopNews = newsBusiness.GetListTopNews();
            return View();
        }
    }
}