using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.Helper;
using WebsiteBanHangCongNghe.ViewModel;
using X.PagedList;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly QlbhcongNgheContext db;

        public ProductController(QlbhcongNgheContext context) => db = context;



        public IActionResult Index(int? categoryId, int? brandId, string search, int? page)
        {
            IQueryable<Product> query = db.Products
                                            .Include(p => p.Category)
                                            .Include(p => p.Brand)
                                            .Include(p => p.Instock)
                                            .OrderByDescending(p => p.Id);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (brandId.HasValue)
            {
                query = query.Where(p => p.BrandId == brandId.Value);
            }

            int totalProducts = query.Count();
            ViewBag.TotalProducts = totalProducts;
            ViewBag.Search = search;

            int pageSize = 6;
            int pageNumber = page ?? 1;
            var list = query.ToPagedList(pageNumber, pageSize);

            ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name", categoryId);
            ViewBag.Brands = new SelectList(db.Brands.ToList(), "Id", "Name", brandId);

            return View(list);
        }



        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.Brand = new SelectList(db.Brands.ToList(), "Id", "Name");
            ViewBag.Instock = new SelectList(db.Instocks.ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile Imgs)
        {
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Name", product.CategoryId);
            ViewBag.Brand = new SelectList(db.Brands.ToList(), "Id", "Name", product.BrandId);
            ViewBag.Instock = new SelectList(db.Instocks.ToList(), "Id", "Name", product.InstockId);
            if (product != null)
            {
                try
                {
                    var existingProduct = db.Products.FirstOrDefault(p => p.Name == product.Name);
                    if (existingProduct != null)
                    {

                        ModelState.AddModelError("Name", "Sản phẩm đã tồn tại.");
                        return View(product);
                    }
                    if (Imgs != null)
                    {
                        product.Imgs = MyUltil.SaveImage(Imgs, "Product");
                    }
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product");
                }
                catch (Exception ex) {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi tạo sản phẩm.");
                }

            }

            return View();
        }
        [Route("Edit")]

        public IActionResult Edit(int id)
        {

            var product = db.Products.Find(id);
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.Brand = new SelectList(db.Brands.ToList(), "Id", "Name");
            ViewBag.Instock = new SelectList(db.Instocks.ToList(), "Id", "Name");
            return View(product);
        }
        [Route("Edit")]

        [HttpPost]
        [ValidateAntiForgeryToken] // Ensure anti-forgery token validation
    public IActionResult Edit(int id, Product product, IFormFile Imgs)
        {
            ModelState.Remove("Imgs");
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Name", product.CategoryId);
            ViewBag.Brand = new SelectList(db.Brands.ToList(), "Id", "Name", product.BrandId);
            ViewBag.Instock = new SelectList(db.Instocks.ToList(), "Id", "Name", product.InstockId);

            if (product != null)
            {
                var existingProduct = db.Products.SingleOrDefault(p => p.Id == id);

                if (existingProduct != null)
                {
                    if (Imgs == null)
                    {
                        // Nếu không tải lên hình mới, sử dụng ảnh gốc của sản phẩm
                        product.Imgs = existingProduct.Imgs;
                    }
                    else
                    {
                        product.Imgs = MyUltil.SaveImage(Imgs, "Product");
                    }

                    // Cập nhật thông tin của existingProduct với thông tin của product
                    existingProduct.Name = product.Name;
                    existingProduct.ShortDescription = product.ShortDescription;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Imgs = product.Imgs;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.BrandId = product.BrandId;
                    existingProduct.InstockId = product.InstockId;

                    // Cập nhật thực thể trong DbContext
                    db.Update(existingProduct);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy sản phẩm cần sửa.");
                }
            }

            return View(product);
        }

        [Route("Delete")]

        public IActionResult Delete(int id)
        {
            var productToDelete = db.Products.FirstOrDefault(p => p.Id == id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            return View(productToDelete);
        }
        [Route("Delete")]

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var productToDelete = db.Products.FirstOrDefault(p => p.Id == id);
                if (productToDelete != null)
                {
                    // Kiểm tra xem sản phẩm đã được sử dụng trong bất kỳ đơn hàng nào chưa
                    if (db.IsProductInOrders(id))
                    {
                        ModelState.AddModelError("", "Không thể xóa sản phẩm đã được sử dụng trong đơn hàng.");
                        return View("Index", db.Products.ToList());
                    }

                    // Xóa hình ảnh từ hệ thống tệp
                    MyUltil.DeleteImage(productToDelete.Imgs);

                    // Xóa sản phẩm từ cơ sở dữ liệu
                    db.Products.Remove(productToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Product");
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
                return View("Index", db.Products.ToList());
            }
        }

    }



}
