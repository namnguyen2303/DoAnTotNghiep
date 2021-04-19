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
using Data.DB;

namespace APIProject.Controllers
{
    public class ProductDetailController : BaseController
    {
        TranDungShopEntities _db = new TranDungShopEntities();

        // GET: ProductDetail
        public ActionResult Index(int ID, int? Category_ID)
        {
            var item = _db.products.Find(ID);
            ViewBag.ProductDetail = productBusiness.ProductDetail(ID);
            if (Category_ID != null)
            {
                ViewBag.ProductRelated = productBusiness.ProductRelated(Category_ID.Value);
            }
            else
            {
                ViewBag.ProductRelated = productBusiness.ProductRelated(item.product_category_id);
            }
            return View(item);
        }


    }
}