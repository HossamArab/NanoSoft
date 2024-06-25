using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NanoSoftCode.Areas.DashBoard.Controllers
{
    [Area("Dashboard")]
    public class SubCategoryController : Controller
    {
        public SubCategoryController(ISubCategoryService subcategoriesService, ICategoryService categoriesService)
        {
            _subcategoriesService = subcategoriesService;
            _categoriesService = categoriesService;
        }
        private readonly ICategoryService _categoriesService;
        private readonly ISubCategoryService _subcategoriesService;
        public IActionResult Index()
        {
            var subcategory = _subcategoriesService.GetAll();
            return View(subcategory);
        }
        public IActionResult CreateUpdate(int? Id)
        {
            SubCategoryFormViewModel? viewModel;
            
            if (Id is null || Id == 0)
            {
                viewModel =
                new()
                {
                    Categories = _categoriesService.GetSelectList()
                };
                return View(viewModel);
            }
            else
            {
                var subcategory = _subcategoriesService.GetbyId(Id);
                if (subcategory is null) 
                {
                    return NotFound();
                }
                viewModel =
                new()
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name,
                    Categories = _categoriesService.GetSelectList(),
                    CategoryId = subcategory.CategoryId
                };
                return View(viewModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate(SubCategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                return View(model);
            }
            if(model.Id is null || model.Id == 0) 
            {
                await _subcategoriesService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var subcategory = await _subcategoriesService.Update(model);
                if (subcategory is null)
                    return BadRequest();

                return RedirectToAction(nameof(Index));
            }
            
        }
    }
}
