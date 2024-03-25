
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.Helper;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.Controllers
{
	public class CartController : Controller
	{
		private readonly QlbhcongNgheContext db;

		public CartController(QlbhcongNgheContext context) => db = context;
		public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();	

		public IActionResult Index()
		{
			return View(Cart);
		}
		public IActionResult AddToCart(int id, int Quantity = 1)
		{
			var newCart = Cart;
			var item = newCart.SingleOrDefault(p=>p.ProductId == id);
			if(item == null) { 
				var product = db.Products.SingleOrDefault(p=>p.Id == id);
				if (product == null)
				{
					TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
					return Redirect("/404");
				}
				item = new CartItem { 
					ProductId = id, 
					ProductName = product.Name, 
					Image = product.Imgs, 
					Price = product.Price, 
					Quantity =Quantity 
				};
				newCart.Add(item);
			}
			else
			{
				item.Quantity += Quantity;
			}
			HttpContext.Session.Set(MySetting.CART_KEY, newCart);
            return RedirectToAction("Index");



		}
	}
}
