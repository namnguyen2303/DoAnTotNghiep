using APIProject.Areas.Admin.Models;
using APIProject.Controllers;
using Data.DB;
using Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace APIProject.Areas.Admin.Controllers
{
    public class SecurityController : BaseController
    {
        TranDungShopEntities _db = new TranDungShopEntities();
        // GET: Admin/Identity
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var res = _db.customers.SingleOrDefault(x => x.email.ToUpper() == model.email);
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
            foreach (var item in res.userRoles)
            {
                var x = _db.roles.Find(item.roleId);
                roles += x.code + ",";
            }
            if (roles.Length > 1)
            {
                roles = roles.Substring(0, roles.Length - 1);
            }
            SetRoles(model.email, roles);
            return RedirectToAction("ListUser");
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var check = _db.customers.SingleOrDefault(x => x.email.ToUpper() == model.email.ToUpper());
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
                };

                var cusType = _db.customerTypes.FirstOrDefault(x => x.code == "MEMBER");
                if (cusType != null) user.CustomerTypeId = cusType.id;

                user.pass = HtmlHelpers.ComputeHash(model.pass, "SHA256", null);
                _db.customers.Add(user);
                _db.SaveChanges();

                role role = _db.roles.SingleOrDefault(x => x.code.ToUpper() == "MEMBER");
                if (role != null)
                {
                    var userRole = new userRole()
                    {
                        roleId = role.id,
                        customerId = user.id
                    };
                    _db.userRoles.Add(userRole);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
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

            var customer = _db.customers.FirstOrDefault(x => x.email == email);
            return customer;
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ListUser");
        }

        [HttpGet]
        public ActionResult ListUser(int? page)
        {

            var data = _db.customers.OrderBy(x => x.id).ToPagedList(page ?? 1, 20);
            ViewBag.Roles = _db.roles;
            return View(data);
        }

        [HttpGet]
        public ActionResult GetRoleForUser(int id)
        {
            var item = _db.customers.FirstOrDefault(x => x.id == id);
            if (item != null)
            {
                ViewBag.Roles = _db.roles.AsEnumerable();
                return View(item);
            }
            return HttpNotFound("Khong tim thay");
        }

        [HttpPost]
        public ActionResult GetRoleForUser(customer model, List<int> RoleIds)
        {

            //var userRoles = db.UserRoles.Where(x => x.UserId == model.MaND).ToList();
            foreach (var id in RoleIds)
            {
                var userRole = new userRole()
                {
                    roleId = id,
                    customerId = model.id
                };
                _db.userRoles.AddOrUpdate(userRole);
            }
            _db.SaveChanges();
            return RedirectToAction("ListUser");
        }

    }
}