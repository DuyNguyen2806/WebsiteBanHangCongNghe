using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.Helper;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand brand, IFormFile Imgs)
        {
            var existingBrand = db.Brands.FirstOrDefault(b => b.Name == brand.Name);

            if (existingBrand != null)
            {
                // If a brand with the same name already exists, add a model error
                ModelState.AddModelError("Name", "Brand name already exists");
            }
            if(Imgs != null)
            {
                brand.Imgs = MyUltil.SaveImage(Imgs,"Brand");
            }
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index", "Brand");
            }
            
            return View(brand);
        }
        [Route("Brand/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var brand = db.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [Route("Brand/Edit/{id}")]
        [HttpPost]
        public IActionResult Edit(int id, Brand brand, IFormFile Imgs)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            var existingBrand = db.Brands.FirstOrDefault(b => b.Name == brand.Name && b.Id != id);

            if (existingBrand != null)
            {
                ModelState.AddModelError("Name", "Brand name already exists");
            }

            try
            {
                var brandToUpdate = db.Brands.Find(id);

                if (brandToUpdate == null)
                {
                    return NotFound();
                }

                brandToUpdate.Name = brand.Name;
                brandToUpdate.ShortDescription = brand.ShortDescription;
                brandToUpdate.Description = brand.Description;

                if (Imgs != null)
                {
                    // Xóa ảnh cũ trước khi lưu ảnh mới
                    MyUltil.DeleteImage(brandToUpdate.Imgs);

                    // Lưu ảnh mới và cập nhật đường dẫn vào thuộc tính Imgs
                    brandToUpdate.Imgs = MyUltil.SaveImage(Imgs, "Brand");
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Brand");
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ một cách thích hợp, như ghi log hoặc hiển thị thông báo lỗi
                ModelState.AddModelError("", "Error occurred while updating the brand.");
            }

            return View(brand); // Nếu trạng thái của model không hợp lệ, trả về view chỉnh sửa với các lỗi xác thực
        }




        [Route("Brand/Delete/{id}")]

        public IActionResult Delete(int id)
        {
            var brandToDelete = db.Brands.FirstOrDefault(p => p.Id == id);
            if (brandToDelete == null)
            {
                return NotFound();
            }

            return View(brandToDelete);
        }
        [Route("Brand/Delete/{id}")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var brandToDelete = db.Brands.FirstOrDefault(p => p.Id == id);
                if (brandToDelete != null)
                {
            

                    MyUltil.DeleteImage(brandToDelete.Imgs);

                    // Xóa sản phẩm từ cơ sở dữ liệu
                    db.Brands.Remove(brandToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Brand");
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
                return View("Index", db.Brands.ToList());
            }
        }


    }

}

