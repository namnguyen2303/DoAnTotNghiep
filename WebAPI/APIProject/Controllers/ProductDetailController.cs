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

namespace APIProject.Controllers
{
    public class ProductDetailController : BaseController
    {
        // GET: ProductDetail
        public ActionResult Index(int ID, int Category_ID)
        {
            ViewBag.ProductDetail = productBusiness.ProductDetail(ID);
            ViewBag.ProductRelated = productBusiness.ProductRelated(Category_ID);
            return View();
        }
    }
}