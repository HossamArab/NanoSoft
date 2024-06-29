

using Microsoft.EntityFrameworkCore;

namespace NanoSoftCode.Services
{
    public class ProductService : IProductService
    {
        public ProductService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{ImageSettings.ImagesPath}";
            _datasheetPath = $"{_webHostEnvironment.WebRootPath}{DatasheetsSettings.DataSheetPath}";
            _driverpath = $"{_webHostEnvironment.WebRootPath}{DriversSetting.DriverPath}";
        }
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        private readonly string _datasheetPath;
        private readonly string _driverpath;
        public async Task Create(ProductFormViewModel model)
        {
            var hasDatasheet = model.Datasheet is not null;
            var hasDriver = model.Driver is not null;
            var hasImage = model.ImagePath is not null;
            string? imageName = string.Empty;
            string? datasheetsname = string.Empty;
            string? driverssname = string.Empty;
            if (hasDatasheet) 
            {
                datasheetsname = await SaveFiles(model.Datasheet!, _datasheetPath);
            }
            if (hasDriver) 
            {
                driverssname = await SaveFiles(model.Driver!, _driverpath);
            }
            if (hasImage) 
            {
                imageName = await SaveFiles(model.ImagePath!, _imagesPath);
            }
             
            Product product = new()
            {
                Name = model.Name,
                Description = model.Description,
                SubCategoryId = model.SubCategoryId,
                CategoryId = model.CategoryId,
                ImagePath = imageName,
                Datasheet= datasheetsname,
                Driver= driverssname,
            };
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public Task Delete(ProductFormViewModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                .Include(c => c.Category)
                .Include(s => s.SubCategory)
               .AsNoTracking()
               .ToList();
        }

        public Product? GetbyId(int? id)
        {
            return _context.Products
                .Include(c => c.Category)
                .Include(s => s.SubCategory)
                .AsNoTracking()
                .ToList()
                .SingleOrDefault(p => p.Id == id);
        }
        public async Task<Product?> Update(ProductFormViewModel model)
        {
            var product = _context.Products.Find(model.Id);
            if (product is null)
                return null;

            var hasNewImage = model.ImagePath is not null;
            var OldImage = product.ImagePath;
            var hasNewDatasheet = model.Datasheet is not null;
            var OldDataSheet = product.Datasheet;
            var hasNewDriver = model.Driver is not null;
            var OldDriver = product.Driver;
            product.Name = model.Name;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.SubCategoryId = model.SubCategoryId;
            if (hasNewImage)
            {
                product.ImagePath = await SaveFiles(model.ImagePath!, _imagesPath);
            }
            if (hasNewDriver)
            {
                product.Driver = await SaveFiles(model.Driver!, _driverpath);
            }
            if (hasNewDatasheet)
            {
                product.Datasheet = await SaveFiles(model.Datasheet!, _datasheetPath);
            }
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasNewImage)
                {
                    var cover = Path.Combine(_imagesPath, OldImage!);
                    File.Delete(cover);
                }
                if (hasNewDriver)
                {
                    var cover = Path.Combine(_driverpath, OldDriver!);
                    File.Delete(cover);
                }
                if (hasNewDatasheet)
                {
                    var cover = Path.Combine(_datasheetPath, OldDataSheet!);
                    File.Delete(cover);
                }
                return product;
            }
            else
            {
                var Image = Path.Combine(_imagesPath, product.ImagePath!);
                File.Delete(Image);
                var Datashhet = Path.Combine(_datasheetPath, product.Datasheet!);
                File.Delete(Datashhet);
                var Driver = Path.Combine(_driverpath, product.Driver!);
                File.Delete(Driver);
                return null;
            }
        }
        private async Task<string> SaveFiles(IFormFile? Files, string? filePath)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Files.FileName)}";
            var path = Path.Combine(filePath, fileName);
            using var stream = File.Create(path);
            await Files.CopyToAsync(stream);
            return fileName;
        }
    }
}
