
namespace NanoSoftCode.Areas.DashBoard.Controllers
{
    [Area("Dashboard")]
    public class ProductsController : Controller
    {
        public ProductsController(ICategoryService categoriesService, ISubCategoryService subcategoriesService, IProductService productService, AppDbContext context)
        {
            _categoriesService = categoriesService;
            _subcategoriesService = subcategoriesService;
            _productService = productService;
            _context = context;
        }
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoriesService;
        private readonly ISubCategoryService _subcategoriesService;
        private readonly IProductService _productService;
        public IActionResult Index()
        {
            var product = _productService.GetAll();
            return View(product);
        }
        
        [HttpGet]
        public IActionResult CreateUpdate(int? Id)
        {
            ProductFormViewModel? viewModel;

            if (Id is null || Id == 0)
            {
                viewModel =
                new()
                {
                    Categories = _categoriesService.GetSelectList(),
                    SubCategories = _subcategoriesService.GetSelectList()
                };
                return View(viewModel);
            }
            else
            {
                var product = _productService.GetbyId(Id);
                if (product is null)
                {
                    return NotFound();
                }
                viewModel =
                new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description!,
                    Categories = _categoriesService.GetSelectList(),
                    CategoryId = product.CategoryId,
                    CurrentImage = product.ImagePath,
                    CurrentDriver = product.Driver,
                    CurrentDataSheet = product.Datasheet,
                };
                return View(viewModel);
            }
        }
        [HttpGet]
        public JsonResult GetSubCategories(int categoryId)
        {
            var subCategories = _context.SubCategories
                                    .Where(sc => sc.CategoryId == categoryId)
                                    .Select(sc => new SubCategoryFormViewModel
                                    {
                                        Id = sc.Id,
                                        Name = sc.Name,
                                    }).ToList();
            return Json(subCategories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.SubCategories = _subcategoriesService.GetSelectList();
                return View(model);
            }
            if (model.Id is null || model.Id == 0)
            {
                await _productService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var productService = await _productService.Update(model);
                if (productService is null)
                    return BadRequest();

                return RedirectToAction(nameof(Index));
            }

        }
    }
}
