
using NanoSoftCode.Settings;

namespace NanoSoftCode.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{ImageSettings.ImagesPath}";
        }
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Categories
               .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
               .OrderBy(c => c.Text)
               .AsNoTracking()
               .ToList();
        }
        public IEnumerable<Category> GetAll()
        {
            return _context.Categories
                .AsNoTracking()
                .ToList();
        }
        public Category? GetbyId(int? id)
        {
            return _context.Categories
                .AsNoTracking()
                .ToList()
                .SingleOrDefault(c => c.Id == id);
        }
        public async Task Create(CategoryFormViewModel model)
        {
            var imageName = await SaveImage(model.ImagePath);
            //stream.Dispose();
            Category category = new()
            {
                Name = model.Name,
                ImagePath = imageName,
            };
            _context.Add(category);
            _context.SaveChanges();
        }

        public async Task<Category?> Update(EditCategoryFormViewModel model)
        {
            var category = _context.Categories.Find(model.Id);
            if(category is null)
                return null;

            var hasNewImage = model.ImagePath is not null;
            var OldImage = category.ImagePath;
            category.Name = model.Name;
            if(hasNewImage)
            {
                category.ImagePath = await SaveImage(model.ImagePath!);
            }
            var effectedRows =  _context.SaveChanges();
            if(effectedRows > 0)
            {
                if(hasNewImage)
                {
                    var cover = Path.Combine(_imagesPath, OldImage!);
                    File.Delete(cover);
                }
                return category;
            }
            else 
            {
                var cover = Path.Combine(_imagesPath, category.ImagePath!);
                File.Delete(cover);
                return null; 
            }
        }

        public Task Delete(CategoryFormViewModel model)
        {
            throw new NotImplementedException();
        }
        private async Task<string> SaveImage(IFormFile imagepath)
        {
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(imagepath.FileName)}";
            var path = Path.Combine(_imagesPath, imageName);
            using var stream = File.Create(path);
            await imagepath.CopyToAsync(stream);
            return imageName;
        }
    }

}
