using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.Helper;
using X.PagedList;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly QlbhcongNgheContext db;

        public CategoryController(QlbhcongNgheContext context) => db = context;

        public IActionResult Index(int? page)
        {
            IQueryable<Category> query = db.Categories.OrderByDescending(c => c.Id);
            int pageSize = 6;
            int pageNumber = page ?? 1;
            int totalCategory = query.Count();
            ViewBag.totalCategory = totalCategory;
            var list = query.ToPagedList(pageNumber, pageSize);

            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            // Check if a category with the same name already exists
            var existingCategory = db.Categories.FirstOrDefault(c => c.Name == category.Name);

            if (existingCategory != null)
            {
                // If a category with the same name already exists, add a model error
                ModelState.AddModelError("Name", "Category name already exists");
            }

            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [Route("EditCategory")]

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [Route("EditCategory")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(category);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }
        [Route("Category/Delete/{id}")]

        public IActionResult Delete(int id)
        {
            var categoryToDelete = db.Categories.FirstOrDefault(p => p.Id == id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }

            return View(categoryToDelete);
        }
        [Route("Category/Delete/{id}")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var categoryToDelete = db.Categories.FirstOrDefault(p => p.Id == id);
                if (categoryToDelete != null)
                {
                    // Kiểm tra xem sản phẩm đã được sử dụng trong bất kỳ đơn hàng nào chưa
                   

                   
                    // Xóa sản phẩm từ cơ sở dữ liệu
                    db.Categories.Remove(categoryToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa sản phẩm.");
                return View("Index", db.Categories.ToList());
            }
        }

    }
}
