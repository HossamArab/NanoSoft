
namespace NanoSoftCode.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        public SubCategoryService(AppDbContext context)
        {
            _context = context;
        }
        private readonly AppDbContext _context;
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.SubCategories
               .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
               .OrderBy(c => c.Text)
               .AsNoTracking()
               .ToList();
        }
        public IEnumerable<SubCategory> GetAll()
        {
            return _context.SubCategories
               .Include(c => c.Category)
               .AsNoTracking()
               .ToList();

        }
        public SubCategory? GetbyId(int? id)
        {
            return _context.SubCategories
               .Include(c => c.Category)
               .AsNoTracking()
               .SingleOrDefault(s => s.Id == id);
        }
        public async Task Create(SubCategoryFormViewModel model)
        {
            SubCategory subCategory = new()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
            };
            await _context.AddAsync(subCategory);
            await _context.SaveChangesAsync();
        }
        public async Task<SubCategory?> Update(SubCategoryFormViewModel model)
        {
            var subcategory = _context.SubCategories.Find(model.Id);
            if (subcategory is null)
                return null;
            subcategory.Name = model.Name;
            subcategory.CategoryId = model.CategoryId;
            var effectedRows = await _context.SaveChangesAsync();
            if (effectedRows > 0)
            {
                return subcategory;
            }
            else
            {
                return null;
            }
        }
        public Task Delete(SubCategoryFormViewModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategory> GetByCategory(int? Id)
        {
            return _context.SubCategories
               .Include(c => c.Category)
               .Where(sc => sc.CategoryId == Id)
               .AsNoTracking()
               .ToList();
        }
    }
}
