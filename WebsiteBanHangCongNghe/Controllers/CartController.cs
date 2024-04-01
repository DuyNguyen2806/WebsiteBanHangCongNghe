
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Security.Claims;
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
		[Authorize]

		public IActionResult Index()
		{
			return View(Cart);
		}
		[Authorize]
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
				TempData["SuccessMessage"] = "Thêm sản phẩm vào giỏ hàng thành công!";
			}
			else
			{
				item.Quantity += Quantity;
			}
			HttpContext.Session.Set(MySetting.CART_KEY, newCart);
            return RedirectToAction("Index","Product");



		}
		public IActionResult Decrease(int id)
		{
			var newCart = Cart;
			CartItem cartItem = newCart.Where(c => c.ProductId == id).FirstOrDefault();
			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				Cart.RemoveAll(p => p.ProductId == id);
			}
			if (Cart.Count == 0)
			{
				HttpContext.Session.Remove(MySetting.CART_KEY);

			}
			else
			{
				HttpContext.Session.Set(MySetting.CART_KEY, newCart);

			}
			return RedirectToAction("Index");
		}
		public IActionResult Increase(int id)
		{
			var newCart = Cart;
			CartItem cartItem = newCart.Where(c=>c.ProductId == id).FirstOrDefault();
			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				Cart.RemoveAll(p=>p.ProductId == id);
			}
			if(Cart.Count == 0)
			{
				HttpContext.Session.Remove(MySetting.CART_KEY);

			}
			else
			{
				HttpContext.Session.Set(MySetting.CART_KEY, newCart);

			}
			return RedirectToAction("Index");
		}
		
		public IActionResult RemoveCart(int id) {
			var newCart = Cart;
			var item = newCart.SingleOrDefault(p => p.ProductId == id);
			if(item != null) {
				newCart.Remove(item);
				HttpContext.Session.Set(MySetting.CART_KEY , newCart);
			
			}
			return RedirectToAction("Index");

		}
		public IActionResult CheckOut()
		{
			var userName = User.FindFirstValue(MySetting.CLAIM_Username);

			if (userName == null)
			{
				return RedirectToAction("Login", "Access");
			}
			else
			{
				var user = db.Users.FirstOrDefault(u => u.Username == userName);

				if (user == null)
				{
					// Handle case where user is not found
					return RedirectToAction("Login", "Access");
				}

				var orderItem = new Order();
				orderItem.UserId = user.Id;
				orderItem.Username = userName;
				orderItem.StatusId = 1;
				orderItem.Dateorder = DateTime.Now;
				orderItem.PayId = 1;
				orderItem.DeliveryId = 1;
				orderItem.Fee = 0;

				db.Orders.Add(orderItem);
				db.SaveChanges();
			


				foreach (var item in Cart)
				{
					var orderDetail = new OrderDetail();
					orderDetail.ProductId = item.ProductId;
					orderDetail.OrderId = orderItem.Id;
					orderDetail.Price = item.Price;
					orderDetail.Quantity = item.Quantity;
					db.OrderDetails.Add(orderDetail);
					db.SaveChanges();
				}
				HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());
				TempData["SuccessMessage"] = "Đặt hàng thành công!";
				return RedirectToAction("Index");
			}
		}

	}
}
