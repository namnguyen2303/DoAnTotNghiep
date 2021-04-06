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

namespace APIProject.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
       /* [UserAuthenticationFilter]*/
        public ActionResult Index()
        {
            return View();
        }

        [UserAuthenticationFilter]
        public PartialViewResult Search(int Page, string FromDate, string ToDate, string Phone)
        {
            ViewBag.FromDateCus = FromDate;
            ViewBag.ToDateCus = ToDate;
            ViewBag.PhoneSearch = Phone;
            IPagedList<ListCustomerOutputModel> list = cusBusiness.Search(FromDate, ToDate, Phone).ToPagedList(Page, SystemParam.MAX_ROW_IN_LIST_WEB);
            return PartialView("_ListCustomer", list);
        }

        [UserAuthenticationFilter]
        public int DeleteCustomer(int ID)
        {
            return cusBusiness.DeleteCustomer(ID);
        }

        //[UserAuthenticationFilter]
        //public int ResetCus(int ID)
        //{
        //    try
        //    {
        //        return userBusiness.ResetUser(ID);
        //    }
        //    catch
        //    {
        //        return SystemParam.ERROR;
        //    }
        //}
    }
}