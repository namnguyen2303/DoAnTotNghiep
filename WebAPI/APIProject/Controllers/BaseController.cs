﻿using Data.Business;
using Data.DB;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace APIProject.Controllers
{
    public class BaseController : Controller
    {
        protected TranDungShopEntities Context;
        public LoginBusiness loginBusiness;
        public CustomerBusiness cusBusiness;
        public OrderBusiness orderBus;
        public UserBusiness userBusiness;
        public ProductBusiness productBusiness;
        public NewsBusiness newsBusiness;
        public CategoryBusiness categoryBusiness;
        public SlideBusiness slideBusiness;

        //public ListCategoryOutputModel model;
   
        public BaseController() : base()
        {
            loginBusiness = new LoginBusiness(this.GetContext());
            cusBusiness = new CustomerBusiness(this.GetContext());
            orderBus = new OrderBusiness(this.GetContext());
            userBusiness = new UserBusiness(this.GetContext());
            productBusiness = new ProductBusiness(this.GetContext());
            categoryBusiness = new CategoryBusiness(this.GetContext());
            newsBusiness = new NewsBusiness(this.GetContext());
            slideBusiness = new SlideBusiness(this.GetContext());

            ViewData["Categories0"] = categoryBusiness.getCategories0();
            ViewBag.Categories1 = categoryBusiness.getCategories1();
            ViewBag.Categories2 = categoryBusiness.getCategories2();
        }


        /// <summary>
        /// Create new context if null
        /// </summary>
        public TranDungShopEntities GetContext()
        {
            if (Context == null)
            {
                Context = new TranDungShopEntities();
            }
            return Context;
        }
        public UserDetailOutputModel UserLogins
        {
            get
            {
                return Session[Data.Utils.Sessions.LOGIN] as UserDetailOutputModel;
            }
        }
    }
}