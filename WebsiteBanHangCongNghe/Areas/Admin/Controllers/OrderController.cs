using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteBanHangCongNghe.Data;
using X.PagedList;


namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OrderController : Controller
    {
        private readonly QlbhcongNgheContext db;

        public OrderController(QlbhcongNgheContext context) => db = context;
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page ?? 1;

            var orders = db.Orders
                            .Include(o => o.Status)
                            .Include(o => o.Delivery)
                            .Include(o => o.Pay)
                            .Include(o => o.User)
                            .OrderByDescending(o => o.Dateorder) // Sắp xếp theo ngày đặt hàng giảm dần
                            .ToPagedList(pageNumber, pageSize);

            ViewBag.TotalOrders = orders.TotalItemCount;

            return View(orders);
        }
        [Route("Order/Detail")]
        public IActionResult Detail(int id)
        {
            var order = db.Orders
                          .Include(o => o.Status)
                          .Include(o => o.Delivery)
                          .Include(o => o.Pay)
                          .Include(o => o.User)
                          .Include(o => o.OrderDetails)
                              .ThenInclude(od => od.Product)
                          .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        
		



	}
}
