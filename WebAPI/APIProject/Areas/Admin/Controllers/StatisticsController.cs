using APIProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class StatisticsController : Controller
    {
        private string connectString = ConfigurationManager.ConnectionStrings["TranDungShopEntities"].ToString();
        // GET: Admin/Statistics
        public ActionResult Index()
        {
            ViewBag.order = OrderStatistics();
            ViewBag.topProduct = TopProduct();
            return View();
        }

        OrderStatisticsViewModel OrderStatistics()
        {
            var data = SelectRows("EXEC sp_ThongKeDoanhThu");
            var time = "";
            var items = "";
            var prices = "";
            foreach (DataRow dr in data.Rows)
            {
                time += DateTime.Parse(dr["Time"].ToString()).ToString("MM/yyyy") + ";";
                items += dr["TotalItems"].ToString() + ";";
                prices += dr["TotalPrices"].ToString() + ";";
            }
            var model = new OrderStatisticsViewModel()
            {
                Time = time,
                TotalItems = items,
                TotalPrices = prices
            };
            return model;
        }

        TopProductViewModel TopProduct()
        {
            var data = SelectRows("EXEC sp_ThongKeSanPhamBanChay");
            var name = "";
            var items = "";
            var prices = "";
            foreach (DataRow dr in data.Rows)
            {
                name += dr["product_name"].ToString() + ";";
                items += dr["TotalItems"].ToString() + ";";
                prices += dr["TotalPrices"].ToString() + ";";
            }
            var model = new TopProductViewModel()
            {
                ProductName = name,
                TotalItems = items,
                TotalPrices = prices
            };
            return model;
        }


        private DataTable SelectRows(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(this.connectString))
            {
                DataTable dataset = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, connection);
                adapter.Fill(dataset);
                return dataset;
            }
        }
    }
}