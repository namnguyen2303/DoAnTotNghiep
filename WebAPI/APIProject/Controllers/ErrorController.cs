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
    public class ErrorController : BaseController
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}