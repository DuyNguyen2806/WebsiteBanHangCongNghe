using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteBanHangCongNghe.Data;
using X.PagedList;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UserController : Controller
    {
        private readonly QlbhcongNgheContext db;

        public UserController(QlbhcongNgheContext context) => db = context;
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page ?? 1;
            var users = db.Users.Where(u=>u.RoleId ==2).ToPagedList(pageNumber, pageSize);
            

            return View(users);
        }

        public IActionResult AccountAdmin(int? page)
        {
            int pageSize = 8;
            int pageNumber = page ?? 1;
            var users = db.Users.Where(u => u.RoleId == 1).ToPagedList(pageNumber, pageSize);
            return View(users);
        }
        public IActionResult ActivateAdmin(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                // Cập nhật role của người dùng thành admin (RoleId = 1)
                user.RoleId = 1;
                db.SaveChanges();
            }
            return RedirectToAction("AccountAdmin", "User"); // Chuyển hướng sau khi kích hoạt thành công
        }
        public IActionResult RevokeAdmin(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                
                user.RoleId = 2;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // Handle case where user is not found
                return NotFound();
            }
        }
    }
}
