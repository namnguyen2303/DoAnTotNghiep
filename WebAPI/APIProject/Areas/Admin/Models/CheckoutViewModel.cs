using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIProject.Areas.Admin.Models
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Chọn loại thanh toán")]
        public int paymentId { get; set; }
        public string note { get; set; }
    }
}