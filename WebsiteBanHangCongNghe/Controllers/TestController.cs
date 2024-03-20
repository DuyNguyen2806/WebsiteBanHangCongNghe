using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;

namespace WebsiteBanHangCongNghe.Controllers
{
    public class TestController : Controller
    {
        private readonly QlbhcongNgheContext db;

        public TestController(QlbhcongNgheContext context) => db = context;
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }
    }
}
