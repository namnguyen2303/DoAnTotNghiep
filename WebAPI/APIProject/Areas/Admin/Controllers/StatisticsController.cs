using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIProject.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Admin/Statistics
        public ActionResult Index()
        {
            return View();
        }
    }
}