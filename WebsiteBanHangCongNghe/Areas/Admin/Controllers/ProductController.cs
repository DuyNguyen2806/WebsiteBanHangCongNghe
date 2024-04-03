using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.Helper;
using WebsiteBanHangCongNghe.ViewModel;
using X.PagedList;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
	public class ProductController : Controller
	{
		private readonly QlbhcongNgheContext db;

		public ProductController(QlbhcongNgheContext context) => db = context;
	

		[Area("Admin")]
		public IActionResult Index(int? page)
		{
			var listProduct = db.Products
										.Include(p => p.Category)
										.Include(p=>p.Brand)
										.Include(p=>p.Instock)
                                        .OrderByDescending(p => p.Id)
                                        .ToList();
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<Product> list = new PagedList<Product>(listProduct, pageNumber, pageSize);

            return View(list);
		}
		[Area("Admin")]
		public IActionResult Create()
		{
            ViewBag.Category = new SelectList(db.Categories.ToList(),"Id", "Name");
            ViewBag.Brand = new SelectList(db.Brands.ToList(),"Id", "Name");
            ViewBag.Instock = new SelectList(db.Instocks.ToList(),"Id", "Name");

            return View();
		}
		[Area("Admin")]

        [HttpPost]
        public IActionResult Create(Product product,IFormFile Imgs)
        {
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Name",product.CategoryId);
            ViewBag.Brand = new SelectList(db.Brands.ToList(), "Id", "Name",product.BrandId);
            ViewBag.Instock = new SelectList(db.Instocks.ToList(), "Id", "Name",product.InstockId);
			if(product != null )
			{
				try
				{
                    var existingProduct = db.Products.FirstOrDefault(p => p.Name == product.Name);
                    if (existingProduct != null)
                    {
                        // Nếu sản phẩm đã tồn tại, bạn có thể thực hiện xử lý tương ứng ở đây, ví dụ:
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

        

    }
}
