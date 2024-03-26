using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.Helper;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.Controllers
{
	public class Access : Controller
	{
		private readonly QlbhcongNgheContext db;
		private readonly IMapper _mapper;

		public Access(QlbhcongNgheContext context, IMapper mapper) {
			db = context;
			_mapper = mapper;
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Register(UserRegister userRegister)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (db.Users.Any(u => u.Username == userRegister.Username))
					{
						// Tên người dùng đã tồn tại, xử lý lỗi ở đây
						ModelState.AddModelError("Username", "Tên người dùng đã được sử dụng. Vui lòng chọn một tên người dùng khác.");
						return View(userRegister);
					}
					var user = _mapper.Map<User>(userRegister);
					user.RandomKey = MyUltil.GenerateRamdomKey();
					user.Password = userRegister.Password.ToMd5Hash(user.RandomKey);
					user.RoleId = 2;
					db.Users.Add(user);
					db.SaveChanges();
					return RedirectToAction("Index", "Product");
				}
				catch (Exception)
				{

					throw;
				}
				
			}
			return View(userRegister);

		}
	}
}
