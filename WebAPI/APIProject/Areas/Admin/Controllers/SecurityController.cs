using APIProject.Areas.Admin.Models;
using APIProject.Controllers;
using Data.DB;
using Data.Utils;
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
    [Authorize(Roles = "admin,member")]
    public class SecurityController : BaseController
    {
        TranDungShopEntities _db = new TranDungShopEntities();

        public SecurityController()
        {

        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginAdminViewModel model)
        {
            var res = _db.users.FirstOrDefault(x => x.username.ToUpper() == model.username.ToUpper());

            if (res == null)
            {
                ModelState.AddModelError("", @"Tài khoản này không tồn tại!");
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
                roles = roles.Substring(0, roles.Length - 1).ToLower();
            }
            FormsAuthentication.SignOut();
            SetRoles(model.username, roles);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AddOrUpdate(int? id)
        {
            if (id != null)
            {
                var user = _db.users.Find(id);
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return HttpNotFound("Không tìm thấy");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(user model)
        {
            if (model.username.ToLower() == "admin")
            {
                ModelState.AddModelError("", @"Không được để tên là admin");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                if (model.id == 0)
                {
                    var check = _db.users.Find(model.id);
                    if (check != null)
                    {
                        ModelState.AddModelError("", @"Tài khoản đã được sử dụng");
                        return View(model);
                    }
                }

                model.status = 1;
                model.is_active = 1;
                model.pass = HtmlHelpers.ComputeHash(model.pass, "SHA256", null);
                _db.users.AddOrUpdate(model);
                _db.SaveChanges();

                return RedirectToAction("GetAllUser");
            }
            return View(model);
        }

        public void SetRoles(string userName, string roles)
        {

            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, //time begin
                                                        DateTime.Now.AddDays(3), //timeout
                                                        false, roles,
                                                        FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            Response.Cookies.Add(cookie);
        }

        string getUserName()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name 
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie == null) return null;
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string email = ticket.Name; //You have the UserName!
            return email;
        }

        user GetUserNameFromCookie()
        {
            try
            {
                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
                if (authCookie == null) return null;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string username = ticket.Name; //You have the UserName!
                var customer = _db.users.FirstOrDefault(x => x.username == username);
                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ListUser");
        }

        [HttpGet]
        public ActionResult GetAllUser(int? page)
        {
            var data = _db.users.Where(x => x.is_active == 1).OrderBy(x => x.id).ToPagedList(page ?? 1, SystemParam.PAGE_SIZE);

            var user = GetUserNameFromCookie();
            var roles = "";
            if (user != null)
            {
                if (user.userRoles != null && user.userRoles.Any())
                {
                    foreach (var item in user.userRoles)
                    {
                        roles += item.role.name + ",";
                    }
                }
                ViewBag.roles = roles;
                ViewBag.username = user.username;
            }
            return View(data);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult GetRoleForUser(int id)
        {
            var item = _db.users.Find(id);
            if (item != null)
            {
                item.userRoles = item.userRoles.ToList();
                ViewBag.Roles = _db.roles.AsEnumerable();
                return View(item);
            }
            return HttpNotFound("Khong tim thay");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetRoleForUser(user model, List<int> RoleIds)
        {
            var item = _db.users.Find(model.id);

            if (item != null)
            {
                if (item.userRoles != null && item.userRoles.Any())
                {
                    _db.userRoles.RemoveRange(item.userRoles);
                    _db.SaveChanges();
                }
            }

            foreach (var id in RoleIds)
            {
                var userRole = new userRole()
                {
                    roleId = id,
                    userId = model.id
                };
                _db.userRoles.AddOrUpdate(userRole);
            }

            await _db.SaveChangesAsync();

            if (model.username == getUserName())
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }
            return RedirectToAction("GetAllUser");
        }

        public async Task<ActionResult> DeleteUser(int id)
        {
            var item = _db.users.Find(id);
            if (item != null)
            {
                item.is_active = 0;
                await _db.SaveChangesAsync();

            }
            return RedirectToAction("GetAllUser");
        }
    }
}