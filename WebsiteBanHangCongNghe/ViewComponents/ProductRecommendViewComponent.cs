using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.ViewComponents
{
	public class ProductRecommendViewComponent: ViewComponent
	{
		private readonly QlbhcongNgheContext db;

		public ProductRecommendViewComponent(QlbhcongNgheContext context) => db = context;
		public IViewComponentResult Invoke()
		{
			var data = db.Products.Select(p => new ProductVM
			{
				Id = p.Id,
				Name = p.Name,
				image= p.Imgs,
				price = p.Price,
			});
			var latestProducts = data.OrderByDescending(p => p.Id).Take(3).ToList();

			return View(latestProducts);
		}
	}
}
