using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.ViewModel;
using X.PagedList;

namespace WebsiteBanHangCongNghe.Controllers
{
	public class ProductController : Controller
	{
		private readonly QlbhcongNgheContext db;

		public ProductController(QlbhcongNgheContext context) => db = context;
		public IActionResult Index(int? page,int? category_id, int? brand_id)
		{

			var lstProduct = db.Products.AsQueryable();
			if (category_id.HasValue)
			{
				lstProduct = lstProduct.Where(p=>p.Category.Id == category_id.Value);
			}
            if (brand_id.HasValue)
            {
                lstProduct = lstProduct.Where(p => p.Brand.Id == brand_id.Value);
            }

            int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var result = lstProduct.Select(p => new ProductVM
			{
				Id = p.Id,
				Name = p.Name,
				price = p.Price,
				image = p.Imgs,
				description = p.Description,
			});

			PagedList<ProductVM> list = new PagedList<ProductVM>(result, pageNumber, pageSize);
			ViewBag.Cid = category_id;
			ViewBag.brand_id = brand_id;

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
			var comments = db.Comments.Where(c => c.ProductId == id).ToList();
			var result = new ProductDetailVM
			{
				Id = product.Id,
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
		

		public IActionResult Search(string? search)
        {
            ViewBag.Search = search;
            var lstProduct = db.Products.AsQueryable();
            if (search != null)
            {
                lstProduct = lstProduct.Where(p => p.Name.Contains(search));
            }
            var result = lstProduct.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                price = p.Price,
                image = p.Imgs,
                description = p.Description
            });
            return View(result);
        }
    }
}
