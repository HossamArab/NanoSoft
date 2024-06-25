namespace NanoSoftCode.Services
{
    public interface ICategoryService
    {
        IEnumerable<SelectListItem> GetSelectList();
        IEnumerable<Category> GetAll();
        Category? GetbyId(int? id);
        Task Create(CategoryFormViewModel model);
        Task<Category?> Update(EditCategoryFormViewModel model);
        Task Delete(CategoryFormViewModel model);
    }
}
