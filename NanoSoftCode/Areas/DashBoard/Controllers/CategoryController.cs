
namespace NanoSoftCode.Areas.DashBoard.Controllers
{
    [Area("Dashboard")]
    public class CategoryController : Controller
    {
        public CategoryController(ICategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        private readonly ICategoryService _categoriesService;

        public IActionResult Index()
        {
            var category = _categoriesService.GetAll();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create (CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _categoriesService.Create(model);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? Id)
        {
            var category = _categoriesService.GetbyId(Id);
            if (category is null)
                return NotFound();

            EditCategoryFormViewModel viewModel =
                new()
                {
                    Id = category.Id,
                    Name = category.Name,
                    CurrentImage = category.ImagePath
                };
            return View(viewModel);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditCategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = await _categoriesService.Update(model);
            if (category is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
    }
}
