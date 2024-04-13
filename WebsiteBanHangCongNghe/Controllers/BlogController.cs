using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;
using X.PagedList;

namespace WebsiteBanHangCongNghe.Controllers
{
    public class BlogController : Controller
    {
        private readonly QlbhcongNgheContext db;

        public BlogController(QlbhcongNgheContext context) => db = context;
        public IActionResult Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = page ?? 1;

            var lstBlog = db.Blogs.ToPagedList(pageNumber, pageSize); ;
            return View(lstBlog);
        }
        public IActionResult Detail(int id)
        {
            var blog = db.Blogs.SingleOrDefault(b => b.Id == id);
            return View(blog);
        }
    }
}
