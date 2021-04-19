using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIProject.Areas.Admin.Models
{
    public class RegisterViewModel
    {

        [StringLength(250), Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string pass { get; set; }
        [StringLength(250), Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        public string confimPass { get; set; }
        [StringLength(250), Required(ErrorMessage = "Email không được để trống"), EmailAddress(ErrorMessage = "Không đúng định dạng")]
        public string username { get; set; }
    }
    public class LoginViewModel
    {
        [StringLength(250), Required(ErrorMessage = "Email không được để trống"), EmailAddress(ErrorMessage = "Không đúng định dạng")]
        public string email { get; set; }
        [StringLength(250), Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string pass { get; set; }
    }

    public class LoginAdminViewModel
    {
        [StringLength(250), Required(ErrorMessage = "Email không được để trống"), EmailAddress(ErrorMessage = "Không đúng định dạng")]
        public string username { get; set; }
        [StringLength(250), Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string pass { get; set; }
    }


    public class RegisterHomeViewModel
    {
        [StringLength(50)]
        public string name_customer { get; set; }
        [StringLength(15)]
        public string phone { get; set; }
        [StringLength(250), Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string pass { get; set; }
        [StringLength(250), Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        public string confimPass { get; set; }
        [StringLength(250), Required(ErrorMessage = "Email không được để trống"), EmailAddress(ErrorMessage = "Không đúng định dạng")]
        public string email { get; set; }
    }
}