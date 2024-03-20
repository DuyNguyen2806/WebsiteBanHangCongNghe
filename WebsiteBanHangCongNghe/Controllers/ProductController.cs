using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.ViewModel;
using X.PagedList;

namespace WebsiteBanHangCongNghe.Controllers
{
	public class ProductController : Controller
	{
		private readonly QlbhcongNgheContext db;

		public ProductController(QlbhcongNgheContext context) => db = context;
		public IActionResult Index(int? page)
		{

			var lstHangHoa = db.Products.AsQueryable();
			int pageSize = 3;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var result = lstHangHoa.Select(p => new ProductVM
			{
				Id = p.Id,
				Name = p.Name,
				price = p.Price,
				image = p.Imgs,
				description = p.Description,
			});

			PagedList<ProductVM> list = new PagedList<ProductVM>(result, pageNumber, pageSize);
			return View(list);
		}
		public IActionResult Details(int id)
		{

			var product = db.Products.Include(p=>p.Category).Include(p=>p.Brand).Include(p=>p.Instock)
				.SingleOrDefault(p => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			var result = new ProductDetailVM
			{
				Name = product.Name,
				image = product.Imgs,
				price = product.Price,
				description = product.ShortDescription,
				Rating = product.Rating,
				CategoryName = product.Category.Name,
				BrandName = product.Brand.Name,
				InstockName = product.Instock.Name,
			};

			return View(result);

		}
	}
}
