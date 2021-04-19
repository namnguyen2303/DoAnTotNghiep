using APIProject.Areas.Admin.Models;
using Data.DB;
using Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace APIProject.Controllers
{

    public class CartController : BaseController
    {
        TranDungShopEntities _db = new TranDungShopEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var customer = GetUserNameFromCookie();
            if (customer == null) return RedirectToAction("Login", "Account");
            var cart = _db.carts.FirstOrDefault(x => x.customerId == customer.id);
            return View(cart);
        }

        [HttpPost]
        public async Task<ActionResult> AddCart(int productId, int quantity)
        {
            var user = GetUserNameFromCookie();
            if (user == null) return RedirectToAction("Login", "Account");

            var product = _db.products.Find(productId);
            if (product == null)
            {
                ModelState.AddModelError("", @"Không tìm thấy sản phẩm đã chọn");
                return RedirectToAction("Index", "Home", new { ID = productId, Category_ID = product.product_category_id });
            }
            var cart = _db.carts.FirstOrDefault(x => x.customerId == user.id);
            if (cart == null)
            {

                var cartNew = new cart()
                {
                    customerId = user.id,
                    totalItems = 0,
                    totalPrices = 0,
                };
                _db.carts.Add(cartNew);
                _db.SaveChanges();

                var cartDetail = new cartDetail()
                {
                    cartId = cartNew.id,
                    productId = productId,
                    createDate = DateTime.Now,
                    totalItems = quantity,
                    totalPrices = 0
                };
                _db.cartDetails.Add(cartDetail);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var cd = _db.cartDetails.FirstOrDefault(x => x.cartId == cart.id && x.productId == productId);
                if (cd != null)
                {
                    cd.totalItems += quantity;
                    cart.totalItems += quantity;
                    if (product.price_sale != null)
                    {
                        cd.totalPrices += quantity * product.price_sale.Value;
                        cart.totalPrices += quantity * product.price_sale.Value;
                    }
                    else
                    {
                        cd.totalPrices += quantity * product.price_start.Value;
                        cart.totalPrices += quantity * product.price_start.Value;
                    }
                    _db.carts.AddOrUpdate(cart);
                    _db.cartDetails.AddOrUpdate(cd);
                }
                else
                {
                    var cartDetail = new cartDetail()
                    {
                        cartId = cart.id,
                        productId = productId,
                        createDate = DateTime.Now,
                        totalItems = quantity,
                        totalPrices = 0
                    };
                    _db.cartDetails.Add(cartDetail);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> ChangQuantity(int ProductId, int Quantity)
        {
            var user = GetUserNameFromCookie();
            if (user == null) return RedirectToAction("Login", "Account");
            var product = _db.products.Find(ProductId);
            if (product == null) return Json(false);

            var cart = _db.carts.FirstOrDefault(x => x.customerId == user.id);
            if (cart == null) return Json(false);

            var cartDetail = _db.cartDetails.FirstOrDefault(x => x.productId == ProductId && x.cartId == cart.id);
            if (cartDetail == null) return Json(false);
            decimal price = 0;

            if (product.price_sale != null)
            {
                price = Quantity * product.price_sale.Value;
            }
            else
            {
                price = Quantity * product.price_start.Value;
            }
            cart.totalPrices += (price - cartDetail.totalPrices);
            cart.totalItems = cart.totalItems + (Quantity - cartDetail.totalItems);

            cartDetail.totalPrices = price;
            cartDetail.totalItems = Quantity;
            await _db.SaveChangesAsync();

            var data = new ChangeQuantityCart()
            {
                Prices = price.ToString("#,##") + " VNĐ",
                TotalPrices = cart.totalPrices.ToString("#,##") + " VNĐ",
                TotalItems = cart.totalItems
            };

            return Json(data);

        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> RemoveProduct(int ProductId)
        {
            var user = GetUserNameFromCookie();
            if (user == null) return RedirectToAction("Login", "Account");
            var product = _db.products.Find(ProductId);
            if (product == null) return Json(false);

            var cart = _db.carts.FirstOrDefault(x => x.customerId == user.id);
            if (cart == null) return Json(false);

            var cartDetail = _db.cartDetails.FirstOrDefault(x => x.productId == ProductId && x.cartId == cart.id);
            if (cartDetail == null) return Json(false);

            _db.cartDetails.Remove(cartDetail);
            await _db.SaveChangesAsync();
            return Json(true);
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


        [HttpGet]
        public ActionResult CheckOut()
        {
            var user = GetUserNameFromCookie();
            if (user == null) return RedirectToAction("Login", "Account");
            ViewBag.user = user;
            var cart = _db.carts.FirstOrDefault(x => x.customerId == user.id);
            if (cart == null) return RedirectToAction("Index");
            ViewBag.listPayment = _db.payments.Where(x => x.active);
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CheckOut(CheckoutViewModel model)
        {
            var user = GetUserNameFromCookie();
            if (user == null) return RedirectToAction("Login", "Account");
            var cart = _db.carts.FirstOrDefault(x => x.customerId == user.id);

            if (!cart.cartDetails.Any())
            {
                ModelState.AddModelError("", @"Không có sản phẩm trong giỏ hàng");
                ViewBag.user = user;
                ViewBag.listPayment = _db.payments.Where(x => x.active);
                return View(cart);
            }

            if (ModelState.IsValid)
            {
                var payment = _db.payments.Find(model.paymentId);
                if (payment == null)
                {
                    ModelState.AddModelError("", @"Không có loại thanh toán mà bạn đã chọn");
                    ViewBag.user = user;
                    ViewBag.listPayment = _db.payments.Where(x => x.active);
                    return View(cart);
                }

                var order = new order()
                {
                    customer_id = user.id,
                    created_at = DateTime.Now,
                    paymentId = model.paymentId,
                    status = SystemParam.IS_ORDER,
                    totalItem = cart.totalItems,
                    total_price = cart.totalPrices,
                    note = model.note,
                    date_buy = DateTime.Now
                };
                _db.orders.Add(order);
                _db.SaveChanges();

                foreach (var item in cart.cartDetails)
                {
                    var od = new orderDetail()
                    {
                        orderId = order.id,
                        productId = item.productId,
                        totalItems = item.totalItems,
                        totalPrices = item.totalPrices
                    };
                    _db.orderDetails.Add(od);
                }
                _db.cartDetails.RemoveRange(cart.cartDetails);
                await _db.SaveChangesAsync();
                return RedirectToAction("CheckoutComplete");
            }
            ViewBag.user = user;
            ViewBag.listPayment = _db.payments.Where(x => x.active);
            return View(cart);

        }

        [Authorize]
        public ActionResult CheckoutComplete()
        {
            return View();
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult CartPartialView()
        {
            var user = GetUserNameFromCookie();
            var count = 0;
            if (user != null)
            {
                var cart = _db.carts.FirstOrDefault(x => x.customerId == user.id);
                if (cart != null)
                {
                    count = cart.cartDetails.Sum(x => x.totalItems);
                }
            }

            return PartialView(count);
        }
    }
}