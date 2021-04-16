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
    public class UserBusiness : GenericBusiness
    {
        public UserBusiness(TranDungShopEntities context = null) : base()
        {
        }

        public List<ListCustomerOutputModel> Search(string Phone, string FromDate, string ToDate)
        {
            try
            {
                DateTime? fd = Util.ConvertDate(FromDate);
                DateTime? td = Util.ConvertDate(ToDate);

                List<ListCustomerOutputModel> list = (from u in cnn.users
                                                      where u.is_active.Equals(SystemParam.ACTIVE) && u.role == SystemParam.ROLE_ADMIN
                                                      && (!String.IsNullOrEmpty(Phone) ? u.username.ToLower().Contains(Phone.ToLower()) || u.phone.ToLower().Contains(Phone.ToLower()) : true)
                                                      && (fd.HasValue ? u.created_at >= fd.Value : true)
                                                      && (td.HasValue ? u.created_at <= td.Value : true)
                                                      orderby u.id descending
                                                      select new ListCustomerOutputModel
                                                      {
                                                          ID = u.id,
                                                          Name = u.username,
                                                          PhoneNumber = u.phone,
                                                          CreateDate = u.created_at
                                                      }).ToList();

                return list;
            }
            catch
            {
                return new List<ListCustomerOutputModel>();
            }
        }

        public int Resetusers(string ID)
        {
            try
            {
                user u = cnn.users.Find(ID);
                u.pass = Util.GenPass(SystemParam.RESET_PASSWORD);
                cnn.SaveChanges();
                return SystemParam.SUCCESS;
            }
            catch
            {
                return SystemParam.ERROR;
            }
        }

        public int ChangePassword(int ID, string CurrentPassword, string NewPassword)
        {
            try
            {
                var passusers = cnn.users.Where(u => u.is_active == SystemParam.ACTIVE && u.id == ID).FirstOrDefault();

                if (!Util.CheckPass(CurrentPassword, passusers.pass))
                {
                    return SystemParam.WRONG_PASSWORD;
                }

                user users = cnn.users.Find(ID);
                users.pass = Util.GenPass(NewPassword);
                cnn.SaveChanges();
                return SystemParam.SUCCESS;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        public int CreateUser(string Phone, string usersName, /*string usersEmail, string usersCode,*/ string usersPass)
        {
            try
            {
                var currentusers = cnn.users.Where(u => u.is_active.Equals(SystemParam.ACTIVE) && u.phone.Equals(Phone));
                if ((currentusers != null && currentusers.Count() > 0))
                {
                    return SystemParam.EXISTING;
                }
                //query lấy ra count của bảng user để gán id cho user mới
                var query = cnn.users.Select(u => u);

                user user = new user();
                user.id = query.Count();
                user.phone = Phone;
                user.pass = Util.GenPass(usersPass);
                user.username = usersName;
                user.role = 1;
                user.is_active = SystemParam.ACTIVE;
                user.created_at = DateTime.Now;
                cnn.users.Add(user);
                cnn.SaveChanges();
                return SystemParam.SUCCESS;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        public int Deleteuser(int ID)
        {
            try
            {
                user users = cnn.users.Find(ID);
                users.is_active = SystemParam.ACTIVE_FALSE;
                cnn.SaveChanges();
                return SystemParam.SUCCESS;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return SystemParam.ERROR;
            }
        }

        public UserDetailOutputModel EditUser(int ID)
        {
            try
            {
                UserDetailOutputModel users = new UserDetailOutputModel();
                var query = (from c in cnn.users
                             where c.is_active == SystemParam.ACTIVE
                             && c.id == ID
                             select new UserDetailOutputModel
                             {
                                 UserID = c.id,
                                 UserName = c.username,
                                 Phone = c.phone
                             }).FirstOrDefault();
                if (query != null)
                {
                    return users = query;
                }
                else
                {
                    return users;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new UserDetailOutputModel();
            }
        }

        public int SaveEditUser(int ID, string Name)
        {
            try
            {
                var c = cnn.users.Find(ID);
                c.username = Name;
                //c.updated_at = DateTime.Now;
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
