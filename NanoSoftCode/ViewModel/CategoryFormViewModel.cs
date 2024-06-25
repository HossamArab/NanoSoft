namespace NanoSoftCode.ViewModel
{
    public class CategoryFormViewModel
    {
        [MaxLength(250)]
        [Display(Name = "اسم القسم")]
        [Required(ErrorMessage = "يجب ادخال اسم القسم")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "إضافة صورة")]
        [AllowedExtensions(ImageSettings.AllowedExtentions),
            MaxFileSizeAttibute(ImageSettings.MaxFileSizeInMB)]
        public IFormFile ImagePath { get; set; } = default!;
    }
}
