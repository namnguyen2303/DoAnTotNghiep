using Data.DB;
using Data.Model;
using Data.Model.APIWeb;
using Data.Utils;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Business
{
    public class LoginBusiness : GenericBusiness
    {
        public LoginBusiness(TranDungShopEntities context = null) : base()
        {

        }
        public UserDetailOutputModel CheckLoginWeb(string phone, string password)
        {
            UserDetailOutputModel query = new UserDetailOutputModel();
            var passUser = cnn.users.Where(u => u.is_active == SystemParam.ACTIVE && u.phone == phone ).FirstOrDefault();
            if (passUser == null)
                return query;
            if (Util.CheckPass(password, passUser.pass))
            {
                query = cnn.users.Where(u => u.is_active == SystemParam.ACTIVE && u.phone == phone).Select(u => new UserDetailOutputModel { UserID = u.id, UserName = u.username, Role = u.role, Phone = u.phone }).FirstOrDefault();
            }
            else
            {
                query = null;
            }
            return query;
        }
       
        //public UserDetailOutputModel CheckLoginWeb(string phone, string password)
        //{
        //    UserDetailOutputModel query = new UserDetailOutputModel();
        //    //string newpass = Util.CreateMD5(password);
        //    var cus = cnn.users.Where(u => u.is_active.Equals(SystemParam.ACTIVE) && u.phone.Equals(phone) && u.pass.Equals(password)).Select(u => new UserDetailOutputModel { UserID = u.id, UserName = u.username, Role = u.role, Phone = u.phone });
        //    if (cus != null && cus.Count() > 0)
        //    {
        //        query = cus.FirstOrDefault();
        //    }
        //    else
        //    {
        //        query = null;
        //    }
        //    return query;
        //}
        //public int ListOrder()
        //{
        //    var order = cnn.orders.Where(o => o.is_active == SystemParam.ACTIVE).Count();
        //    return order;
        //}

        //public int ListPost()
        //{
        //    var post = cnn.posts.Where(o => o.is_active.Equals(SystemParam.ACTIVE)).Count();
        //    return post;
        //}

        //public int ListProduct()
        //{
        //    var product = cnn.products.Where(o => o.is_active.Equals(SystemParam.ACTIVE)).Count();
        //    return product;
        //}
    }
}
