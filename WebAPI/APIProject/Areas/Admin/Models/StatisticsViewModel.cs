using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIProject.Areas.Admin.Models
{
    public class OrderStatisticsViewModel
    {
        public string Time { get; set; }
        public string TotalItems { get; set; }
        public string TotalPrices { get; set; }
    }
    public class TopProductViewModel
    {
        public string ProductName { get; set; }
        public string TotalItems { get; set; }
        public string TotalPrices { get; set; }
    }

    public class ChangeQuantityCart
    {
        public string Prices { get; set; }
        public string TotalPrices { get; set; }
        public int TotalItems { get; set; }
    }
}