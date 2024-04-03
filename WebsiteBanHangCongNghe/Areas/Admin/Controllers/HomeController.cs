using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
	public class HomeController : Controller
	{

		private readonly QlbhcongNgheContext db;

	
		public HomeController(QlbhcongNgheContext context) => db = context;
		[Area("Admin")]
		public IActionResult Index()
		{
			return View();
		}
		
	}
}
