using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.Helper;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.Controllers
{
	public class AccessController : Controller
	{
		private readonly QlbhcongNgheContext db;
		private readonly IMapper _mapper;

		public AccessController(QlbhcongNgheContext context, IMapper mapper) {
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
					TempData["SuccessMessage"] = "Registration successful!";
					return RedirectToAction("Index", "Product");
				}
				catch (Exception)
				{

					throw;
				}
				
			}
			return View(userRegister);

		}
		public IActionResult Login(string? returnUrl)
		{
			ViewBag.returnUrl = returnUrl;
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (model != null)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (user == null)
                {
                    ModelState.AddModelError("loi", "The username does not exist");
                    ViewBag.Error = "Incorrect username or password.";
                }
                else
                {
                    if (user.Password != model.Password.ToMd5Hash(user.RandomKey))
                    {
                        ModelState.AddModelError("loi", "Wrong password");
                        ViewBag.Error = "Incorrect username or password.";
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(MySetting.CLAIM_Username, user.Username),
                        };
                        if (user.RoleId == 1)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                        }
                        else
                        {
                            // Đăng nhập cho người dùng không phải admin
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
            }
            return View(model);
        }
        [Authorize]
		public IActionResult Profile()
		{
			return View();
		}
		[Authorize]

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
		public IActionResult AccessDenied()
		{
			return View();
		}

    }
}
