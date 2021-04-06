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
    public class CustomerBusiness : GenericBusiness
    {
        public CustomerBusiness(TranDungShopEntities context = null) : base()
        {
        }
        
        public List<ListCustomerOutputModel> Search(string FromDate, string ToDate, string Phone)
        {
            try
            {
                DateTime? fd = Util.ConvertDate(FromDate);
                DateTime? td = Util.ConvertDate(ToDate);

                List<ListCustomerOutputModel> list = (from cus in cnn.customers
                                                      where cus.is_active.Equals(SystemParam.ACTIVE)
                                                      && (!String.IsNullOrEmpty(Phone) ? cus.name_customer.ToLower().Contains(Phone.ToLower()) || cus.phone.ToLower().Contains(Phone.ToLower()) : true)
                                                      && (fd.HasValue ? cus.created_at >= fd.Value : true)
                                                      && (td.HasValue ? cus.created_at <= td.Value : true)
                                                      orderby cus.id descending
                                                      select new ListCustomerOutputModel
                                                      {
                                                          ID = cus.id,
                                                          Name = cus.name_customer,
                                                          PhoneNumber = cus.phone,
                                                          Email = cus.email,
                                                          Address = cus.address_customer,
                                                          CreateDate = cus.created_at
                                                      }).ToList();

                return list;
            }
            catch
            {
                return new List<ListCustomerOutputModel>();
            }
        }

        public int DeleteCustomer(int ID)
        {
            try
            {
                var cusDelete = cnn.users.Find(ID);
                cusDelete.is_active = SystemParam.ACTIVE_FALSE;
                cnn.SaveChanges();
                return SystemParam.RETURN_TRUE;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.RETURN_FALSE;
            }
        }
    }
}
