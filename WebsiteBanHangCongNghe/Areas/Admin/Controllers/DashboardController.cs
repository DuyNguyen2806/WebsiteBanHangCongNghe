using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WebsiteBanHangCongNghe.Areas.Admin.ViewModel;
using WebsiteBanHangCongNghe.Data;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]


    public class DashboardController : Controller
    {
        private readonly QlbhcongNgheContext db;

        public DashboardController(QlbhcongNgheContext context) => db = context;
        public IActionResult Index()
        {
            var orders = db.Orders
                           .Include(o => o.Status)
                           .Include(o => o.Delivery)
                           .Include(o => o.Pay)
                           .Include(o => o.User)
                           .OrderByDescending(o => o.Dateorder).ToList(); // Sắp xếp theo ngày đặt hàng giảm dần
            var orderDetails = db.OrderDetails
                                .Include(od => od.Product)
                                .ToList();

            // Tính tổng số lượng đã bán
            int totalQuantitySold = orderDetails.Sum(od => od.Quantity);

            // Truyền vào ViewBag
            ViewBag.TotalQuantitySold = totalQuantitySold;

            ViewBag.TotalOrders = orders.Count();
            int totalUsers = db.Users.Where(u => u.RoleId == 2).Count();
            ViewBag.TotalUsers = totalUsers;
            var ordersWithDetails = db.Orders
                            .Include(o => o.OrderDetails)
                                .ThenInclude(od => od.Product)
                            .ToList();

            // Tính tổng số tiền đã bán
            double totalRevenue = ordersWithDetails.Sum(order => order.OrderDetails.Sum(detail => detail.Price * detail.Quantity));
            ViewBag.TotalRevenue = totalRevenue.ToString("N0");
            //total order by month
            var ordersByMonth = GetOrdersByMonth();
            //
            // Tính toán dữ liệu cho biểu đồ tròn
            var revenueByMonth = GetRevenueByMonth();
            ViewBag.RevenueByMonth = revenueByMonth;


            return View(ordersByMonth);
           


        }
        private List<OrdersByMonthViewModel> GetOrdersByMonth()
        {
            var ordersByMonth = db.Orders
                              .GroupBy(o => new { Year = o.Dateorder.Year, Month = o.Dateorder.Month })
                              .Select(g => new { g.Key.Year, g.Key.Month, TotalOrders = g.Count() })
                              .AsEnumerable()
                              .Select(g => new OrdersByMonthViewModel
                              {
                                  MonthYear = $"{g.Month}/{g.Year}",
                                  TotalOrders = g.TotalOrders
                              })
                              .OrderBy(e0 => e0.MonthYear)
                              .ToList();
            return ordersByMonth;
        }

        private List<RevenueByMonthViewModel> GetRevenueByMonth()
        {
            var revenueByMonth = db.OrderDetails
                .Include(od => od.Order)
                .Where(od => od.Order.StatusId == 1 /* ID của trạng thái đã hoàn thành đơn hàng */)
                .GroupBy(od => new { Year = od.Order.Dateorder.Year, Month = od.Order.Dateorder.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, TotalRevenue = g.Sum(od => od.Price * od.Quantity) })
                .AsEnumerable()
                .Select(g => new RevenueByMonthViewModel
                {
                    MonthYear = $"{g.Month}/{g.Year}",
                    TotalRevenue = g.TotalRevenue
                })
                .OrderBy(e0 => e0.MonthYear)
                .ToList();
            return revenueByMonth;
        }
    }
}
