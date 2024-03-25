using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.ViewComponents
{
	public class MenuBrandViewComponent :ViewComponent
	{
		private readonly QlbhcongNgheContext db;

		public MenuBrandViewComponent(QlbhcongNgheContext context) => db = context;
		public IViewComponentResult Invoke()
		{
			
			var data = db.Brands.Select(c => new MenuBrandVM
			{
				id = c.Id,
				name = c.Name,
				quantity = c.Products.Count(),
				
			});
			return View(data);
		}
	}
}
