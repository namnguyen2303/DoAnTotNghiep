using APIProject.App_Start;
using Data.Model;
using Data.Utils;
using Data.DB;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIProject.Controllers;

namespace APIProject.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        [UserAuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult UserLogin(string phone, string password)
        {
            try
            {
                UserDetailOutputModel userLogin = loginBusiness.CheckLoginWeb(phone, password);
                if (userLogin != null && userLogin.UserID > 0)
                {
                    Session[Sessions.LOGIN] = userLogin;
                    return Json(userLogin, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { FAIL_LOGIN = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return Json(new { ERROR = SystemParam.ERROR }, JsonRequestBehavior.AllowGet);
            }
        }

        public int Logout()
        {
            try
            {
                Session[Sessions.LOGIN] = null;
                return SystemParam.SUCCESS;
            }
            catch
            {
                return SystemParam.ERROR;
            }
        }

    }
}