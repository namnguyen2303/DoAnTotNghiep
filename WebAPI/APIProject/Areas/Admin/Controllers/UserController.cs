using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using Data.Model;
using Data.Utils;
using System.Web;
using APIProject.App_Start;
using System.Web.Mvc;
using APIProject.Controllers;

namespace APIProject.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
       /* [UserAuthenticationFilter]*/
        public ActionResult Index()
        {
            return View();
        }

        [UserAuthenticationFilter]
        public PartialViewResult GetUserDetail(/*int ID*/)
        {
            try
            {
                return PartialView("_UserDetail");
            }
            catch
            {
                return PartialView("_UserDetail");
            }
        }

        [UserAuthenticationFilter]
        public PartialViewResult Search(int Page, string Phone, string FromDate, string ToDate)
        {
            try
            {
                ViewBag.FromDate = FromDate;
                ViewBag.ToDate = ToDate;
                ViewBag.Phone = Phone;
                return PartialView("_ListUser", userBusiness.Search(Phone, FromDate, ToDate).ToPagedList(Page, SystemParam.MAX_ROW_IN_LIST_WEB));
            }
            catch
            {
                return PartialView("_ListUser", new List<ListCustomerOutputModel>());
            }
        }

        [UserAuthenticationFilter]
        public int ResetUser(string ID)
        {
            try
            {
                return userBusiness.Resetusers(ID);
            }
            catch
            {
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public int ChangePassword(string CurrentPassword, string NewPassword)
        {
            try
            {
                UserDetailOutputModel userLogin = UserLogins;
                return userBusiness.ChangePassword(userLogin.UserID, CurrentPassword, NewPassword);
            }
            catch
            {
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public int CreateUser(string Phone, string UserName, /*string userEmail, string userCode,*/ string userPass)
        {
            try
            {
                return userBusiness.CreateUser(Phone, UserName, /*userEmail, userCode,*/ userPass);
            }
            catch
            {
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public int DeleteUser(int ID)
        {
            try
            {
                return userBusiness.Deleteuser(ID);
            }
            catch
            {
                return SystemParam.ERROR;
            }
        }

        [UserAuthenticationFilter]
        public PartialViewResult EditUser(int ID)
        {
            try
            {
                UserDetailOutputModel user = userBusiness.EditUser(ID);
                return PartialView("_UserDetail", user);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return PartialView("_UserDetail", new UserDetailOutputModel());
            }
        }

        [UserAuthenticationFilter]
        public int SaveEditUser(int ID, string Name)
        {
            try
            {
                return userBusiness.SaveEditUser(ID, Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }
    }
}