using NanoSoftCode.Models;

namespace NanoSoftCode.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ICategoryService categoriesService) : base(categoriesService)
        {
            _categoriesService = categoriesService;
        }
        private readonly ICategoryService _categoriesService;
        public IActionResult Index()
        {
            var category = _categoriesService.GetAll();
            var viewModel = new CombainLists
            {
                Categories = category
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
