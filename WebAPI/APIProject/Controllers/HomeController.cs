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
using APIProject.Areas.Admin.Controllers;

namespace APIProject.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.listSlide = slideBusiness.ListSlide();
            ViewBag.listProMen = productBusiness.GetListProMen();
            ViewBag.listProWoMen = productBusiness.GetListProWoMen();
            ViewBag.listProKid = productBusiness.GetListProKid();
            ViewBag.listProNew = productBusiness.GetListProNew();
            ViewBag.listProHot = productBusiness.GetListProHot();
            ViewBag.listProSale = productBusiness.GetListProSale();

            ViewBag.listTopNews = newsBusiness.GetListTopNews();
            ViewBag.SpNamHot = productBusiness.getSpNamHot();
            ViewBag.SpHot = productBusiness.getSpHot();


            return View();
        }
    }
}
