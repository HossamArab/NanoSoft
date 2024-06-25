namespace NanoSoftCode.Services
{
    public interface ISubCategoryService
    {
        IEnumerable<SelectListItem> GetSelectList();
        IEnumerable<SubCategory> GetAll();
        SubCategory? GetbyId(int? id);
        Task Create(SubCategoryFormViewModel model);
        Task<SubCategory?> Update(SubCategoryFormViewModel model);
        Task Delete(SubCategoryFormViewModel model);
    }
}
