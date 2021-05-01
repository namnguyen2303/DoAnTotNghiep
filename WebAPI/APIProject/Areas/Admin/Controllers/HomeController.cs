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
using APIProject.Areas.Admin.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace APIProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin,member")]
    public class HomeController : BaseController
    {
        private string connectString = ConfigurationManager.ConnectionStrings["TranDungShopEntities"].ToString();
        TranDungShopEntities _db = new TranDungShopEntities();
        // GET: Admin/Statistics
        public ActionResult Index()
        {
            //ViewBag.order = OrderStatistics();
            //ViewBag.topProduct = TopProduct();
            ViewBag.Post = _db.news.Where(u=>u.is_active == 1).Count();
            ViewBag.Product = productBusiness.GetListPro();
            ViewBag.Order = _db.orders.Where(u=>u.status==1).Count();
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

        [ChildActionOnly]
        public PartialViewResult MenuLeftParitalView()
        {
            var user = GetUserNameFromCookie();
            var roles = "";
            if (user.userRoles.ToList().Count() > 0)
            {
                foreach (var item in user.userRoles)
                {
                    roles += item.role.name + ",";
                }
                ViewBag.roles = roles;
            }

            return PartialView();
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
        OrderStatisticsViewModel OrderStatistics()
        {
            var data = SelectRows("EXEC sp_ThongKeDoanhThu");
            var time = "";
            var items = "";
            var prices = "";
            foreach (DataRow dr in data.Rows)
            {
                time += DateTime.Parse(dr["Time"].ToString()).ToString("MM/yyyy") + ";";
                items += dr["TotalItems"].ToString() + ";";
                prices += dr["TotalPrices"].ToString() + ";";
            }
            var model = new OrderStatisticsViewModel()
            {
                Time = time,
                TotalItems = items,
                TotalPrices = prices
            };
            return model;
        }

        TopProductViewModel TopProduct()
        {
            var data = SelectRows("EXEC sp_ThongKeSanPhamBanChay");
            var name = "";
            var items = "";
            var prices = "";
            foreach (DataRow dr in data.Rows)
            {
                name += dr["product_name"].ToString() + ";";
                items += dr["TotalItems"].ToString() + ";";
                prices += dr["TotalPrices"].ToString() + ";";
            }
            var model = new TopProductViewModel()
            {
                ProductName = name,
                TotalItems = items,
                TotalPrices = prices
            };
            return model;
        }


        private DataTable SelectRows(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(this.connectString))
            {
                DataTable dataset = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, connection);
                adapter.Fill(dataset);
                return dataset;
            }
        }

    }
}