using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.ViewComponents
{
	public class MenuCategoryViewComponent : ViewComponent
	{
		private readonly QlbhcongNgheContext db;

		public MenuCategoryViewComponent(QlbhcongNgheContext context) => db = context;
		public IViewComponentResult Invoke()
		{
			
			var data = db.Categories.Select(c => new MenuCategoryVM
			{
				Id = c.Id,
				name = c.Name,
				quantity = c.Products.Count(p=>p.InstockId !=2),
			});
			return View(data);
		}
 	}
}
