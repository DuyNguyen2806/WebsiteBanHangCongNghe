using Microsoft.AspNetCore.Mvc;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.ViewComponents
{
    public class TopBrandViewComponent :ViewComponent
    {
        private readonly QlbhcongNgheContext db;

        public TopBrandViewComponent(QlbhcongNgheContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.Brands.Select(c => new MenuBrandVM
            {
                id = c.Id,
                name = c.Name,
                img = c.Imgs,

            });
            var latestProducts = data.OrderBy(p => p.id).Take(4).ToList();

            return View(latestProducts);
        }
    }
}
