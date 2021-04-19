using Data.DB;
using Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace APIProject.Controllers
{
    [Authorize]
    public class OrderHistoryController : Controller
    {
        TranDungShopEntities _db = new TranDungShopEntities();
        // GET: OrderHistory
        public ActionResult Index()
        {
            var customer = GetUserNameFromCookie();
            if (customer == null) return RedirectToAction("Login", "Account");
            var orders = _db.orders.Where(x => x.customer_id == customer.id).OrderByDescending(x => x.created_at).AsEnumerable();
            return View(orders);
        }
        customer GetUserNameFromCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            if (authCookie == null) return null;
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string email = ticket.Name;
            var customer = _db.customers.Where(x => x.status == SystemParam.ACTIVE).FirstOrDefault(x => x.email == email);
            return customer;
        }

        public async Task<ActionResult> OrderCancel(int id)
        {
            var order = _db.orders.Find(id);
            if (order != null)
            {
                order.status = SystemParam.IS_HUY_DH;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Không tìm thấy");
        }

        public ActionResult OrderDetail(int id)
        {
            var order = _db.orders.Find(id);
            if (order != null)
            {
                return View(order);
            }
            return RedirectToAction("Index");
        }
    }
}