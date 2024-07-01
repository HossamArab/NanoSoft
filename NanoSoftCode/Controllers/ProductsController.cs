using Microsoft.AspNetCore.Mvc;

namespace NanoSoftCode.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(ICategoryService categoriesService, IProductService productService, ISubCategoryService subCategoryService) : base(categoriesService)
        {
            _categoriesService = categoriesService;
            _productService = productService;
            _subCategoryService = subCategoryService;
        }
        private ICategoryService _categoriesService;
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;
        public IActionResult Index(int? Id)
        {
            var category = _categoriesService.GetAll();
            var products = _productService.GetByCategoryID(Id);
            var subCategories = _subCategoryService.GetByCategory(Id);
            var viewModel = new CombainLists
            {
                Products = products,
                SubCategories = subCategories,
                Categories = category
                
            };

            return View(viewModel);
        }
    }
}
