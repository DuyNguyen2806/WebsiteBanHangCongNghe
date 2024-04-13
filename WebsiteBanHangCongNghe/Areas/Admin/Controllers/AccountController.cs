using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebsiteBanHangCongNghe.Helper;
using WebsiteBanHangCongNghe.ViewModel;
using WebsiteBanHangCongNghe.Data;

namespace WebsiteBanHangCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly QlbhcongNgheContext db;
        public AccountController(QlbhcongNgheContext context) => db = context;
       
       public IActionResult Index()
        {
            return View();  
        }
    }
}
