using Microsoft.AspNetCore.Mvc;

namespace NanoSoftCode.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(ICategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        private readonly ICategoryService _categoriesService;
    }
}
