using APIProject.App_Start;
using Data.Model;
using Data.Utils;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIProject.Controllers;
using Data.DB;
using System.Web.Security;

namespace APIProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, member")]
    public class CustomerController : BaseController
    {
        TranDungShopEntities _db = new TranDungShopEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Search(int Page, string FromDate, string ToDate, string Phone)
        {
            ViewBag.FromDateCus = FromDate;
            ViewBag.ToDateCus = ToDate;
            ViewBag.PhoneSearch = Phone;
            IPagedList<ListCustomerOutputModel> list = cusBusiness.Search(FromDate, ToDate, Phone).ToPagedList(Page, SystemParam.MAX_ROW_IN_LIST_WEB);
            return PartialView("_ListCustomer", list);
        }

        public int DeleteCustomer(int ID)
        {
            return cusBusiness.DeleteCustomer(ID);
        }

        public ActionResult GetAllCustomer(int? page)
        {
            var data = _db.customers.OrderBy(x => x.name_customer).ToPagedList(page ?? 1, SystemParam.PAGE_SIZE);
            var user = GetUserNameFromCookie();
            var roles = "";
            if (user != null)
            {
                if (user.userRoles.ToList().Count() > 0)
                {
                    foreach (var item in user.userRoles)
                    {
                        roles += item.role.name + ",";
                    }
                }
            }
            ViewBag.roles = roles;
            return View(data);
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
    }
}