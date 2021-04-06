using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIProject.Controllers
{
    public class WishlistController : BaseController
    {
        // GET: Wishlist
        public ActionResult Index()
        {
            return View();
        }
    }
}