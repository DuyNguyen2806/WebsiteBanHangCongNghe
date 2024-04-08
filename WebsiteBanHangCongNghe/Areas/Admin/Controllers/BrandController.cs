using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;
using X.PagedList;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly QlbhcongNgheContext db;
        public BrandController(QlbhcongNgheContext context) => db = context;

        public IActionResult Index(int? page)
        {
            IQueryable<Brand> query = db.Brands.OrderByDescending(c => c.Id);
            int pageSize = 6;
            int pageNumber = page ?? 1;
            int totalBrand = query.Count();
            ViewBag.totalCategory = totalBrand;
            var list = query.ToPagedList(pageNumber, pageSize);

            return View(list);
        }

    }
}
