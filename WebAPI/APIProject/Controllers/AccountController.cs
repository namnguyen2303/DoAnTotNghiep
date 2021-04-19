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
using APIProject.Areas.Admin.Models;
using Data.DB;
using Helpers;
using System.Threading.Tasks;
using System.Web.Security;

namespace APIProject.Controllers
{
    public class AccountController : BaseController
    {
        TranDungShopEntities _db = new TranDungShopEntities();
        // GET: Account
        public ActionResult Index()
        {
            string model = null;
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterHomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var check = _db.customers.Where(x => x.status == SystemParam.ACTIVE).FirstOrDefault(x => x.email.ToUpper() == model.email.ToUpper());
                if (check != null)
                {
                    ModelState.AddModelError("", @"Tài khoản đã được sử dụng");
                    return View(model);
                }
                if (model.pass != model.confimPass)
                {
                    ModelState.AddModelError("", @"Mật khẩu xác nhận không khớp nhau");
                    return View(model);
                }

                var user = new customer()
                {
                    created_at = DateTime.Now,
                    phone = model.phone,
                    name_customer = model.name_customer,
                    email = model.email,
                    status = SystemParam.ACTIVE
                };

                var cusType = _db.customerTypes.FirstOrDefault(x => x.code == "MEMBER");
                if (cusType != null) user.CustomerTypeId = cusType.id;

                user.pass = HtmlHelpers.ComputeHash(model.pass, "SHA256", null);
                _db.customers.Add(user);
                await _db.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = _db.customers.Where(x => x.status == SystemParam.ACTIVE).FirstOrDefault(x => x.email.ToUpper() == model.email);
                if (res == null)
                {
                    ModelState.AddModelError("", @"Sai tài khoản");
                    return View(model);
                }
                if (!HtmlHelpers.VerifyHash(model.pass, "SHA256", res.pass))
                {
                    ModelState.AddModelError("", @"Mật khẩu không chính xác.");
                    return View(model);
                }
                string roles = "";
                FormsAuthentication.SignOut();
                SetRoles(model.email, roles);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public void SetRoles(string userName, string roles)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, userName,
                                                        DateTime.Now, //time begin
                                                        DateTime.Now.AddDays(3), //timeout
                                                        false, roles,
                                                        FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            //FormsAuthenticationTicket item = 

            Response.Cookies.Add(cookie);
        }


        customer GetUserNameFromCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie == null) return null;
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string email = ticket.Name; //You have the UserName!

            var customer = _db.customers.Where(x => x.status == SystemParam.ACTIVE).FirstOrDefault(x => x.email == email);
            return customer;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult Menu()
        {
            return PartialView(GetUserNameFromCookie());
        }

    }
}