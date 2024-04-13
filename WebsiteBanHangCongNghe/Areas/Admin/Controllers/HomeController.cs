using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
	[Area("Admin")]


    public class HomeController : Controller
	{

		private readonly QlbhcongNgheContext db;

	
		public HomeController(QlbhcongNgheContext context) => db = context;
		

		public IActionResult Index()
		{
			return View();
		}
		
	}
}
