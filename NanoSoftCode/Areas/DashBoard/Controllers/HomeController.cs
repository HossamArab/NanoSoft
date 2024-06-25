using Microsoft.AspNetCore.Mvc;

namespace NanoSoftCode.Areas.DashBoard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
