using APIProject.App_Start;
using Data.Model.APIWeb;
using Data.Utils;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIProject.Controllers;
using Data.DB;
using System.Threading.Tasks;
using System.Web.Security;

namespace APIProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin,member")]
    public class OrderController : Controller
    {
        TranDungShopEntities _db = new TranDungShopEntities();

        public ActionResult Index(int? page)
        {
            var orders = _db.orders.OrderByDescending(x => x.created_at).ToPagedList(page ?? 1, SystemParam.PAGE_SIZE);
            return View(orders);
        }

        public async Task<ActionResult> OrderProcess(int id)
        {
            var order = _db.orders.Find(id);
            if (order != null)
            {
                order.status = SystemParam.IS_XU_LY;
                var user = GetUserNameFromCookie();
                if (user != null) order.userId = user.id;
                _db.SaveChanges();
                var customer = _db.customers.Find(order.customer_id);
                if (customer != null)
                {
                    var vang = decimal.Parse(SystemParam.DK_VANG.Replace(",", ""));
                    var bac = decimal.Parse(SystemParam.DK_BAC.Replace(",", ""));
                    var kimcuong = decimal.Parse(SystemParam.DK_KIM_CUONG.Replace(",", ""));

                    var totalPices = _db.orders.Where(x => x.status == SystemParam.IS_XU_LY).Sum(x => x.total_price);
                    if (totalPices >= bac)
                    {
                        if (totalPices >= kimcuong)
                        {
                            var type = _db.customerTypes.FirstOrDefault(x => x.code == SystemParam.KIM_CUONG);
                            if (type != null) customer.CustomerTypeId = type.id;
                        }
                        else
                        {
                            if (totalPices >= vang)
                            {
                                var type = _db.customerTypes.FirstOrDefault(x => x.code == SystemParam.VANG);
                                if (type != null) customer.CustomerTypeId = type.id;
                            }
                            else
                            {
                                var type = _db.customerTypes.FirstOrDefault(x => x.code == SystemParam.BAC);
                                if (type != null) customer.CustomerTypeId = type.id;
                            }
                        }
                        await _db.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> OrderCancel(int id)
        {
            var order = _db.orders.Find(id);
            if (order != null)
            {
                order.status = SystemParam.IS_HUY_DH;
                var user = GetUserNameFromCookie();
                if (user != null) order.userId = user.id;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public ActionResult OrderDetail(int id)
        {
            var order = _db.orders.Find(id);
            if (order != null)
            {
                return View(order);
            }
            return HttpNotFound("Không tìm thấy");
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