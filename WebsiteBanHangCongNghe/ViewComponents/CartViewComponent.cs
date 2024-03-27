using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Helper;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.ViewComponents
{
	public class CartViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
			return View("Default", new CartVM
			{
				quantity = cart.Sum(p => p.Quantity),
				total = cart.Sum(p => p.Total)
			});
		}
	}
}
