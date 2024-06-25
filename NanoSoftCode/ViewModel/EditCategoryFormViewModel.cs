namespace NanoSoftCode.ViewModel
{
    public class EditCategoryFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(250)]
        [Display(Name = "اسم القسم")]
        [Required(ErrorMessage = "يجب ادخال اسم القسم")]
        public string Name { get; set; } = string.Empty;
        public string? CurrentImage { get; set; }
        [Display(Name = "إضافة صورة")]
        [AllowedExtensions(ImageSettings.AllowedExtentions),
            MaxFileSizeAttibute(ImageSettings.MaxFileSizeInMB)]
        public IFormFile? ImagePath { get; set; } = default!;
    }
}
