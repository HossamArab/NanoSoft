namespace NanoSoftCode.ViewModel
{
    public class ProductFormViewModel
    {
        public int? Id { get; set; }
        [MaxLength(250)]
        [Display(Name = "اسم المنتج")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2500)]
        [Display(Name = "وصف المنتج")]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "القسم الرئيسي")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "القسم الفرعي")]
        public int SubCategoryId { get; set; }
        public IEnumerable<SelectListItem> SubCategories { get; set; } = Enumerable.Empty<SelectListItem>();
        public string? CurrentImage { get; set; }
        public string? CurrentDataSheet { get; set; }
        public string? CurrentDriver { get; set; }
        public IFormFile? ImagePath { get; set; } = default!;
        public IFormFile? Datasheet { get; set; } = default!;
        public IFormFile? Driver { get; set; } = default!;

    }
}
