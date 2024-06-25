
namespace NanoSoftCode.ViewModel
{
    public class SubCategoryFormViewModel
    {
        public int? Id { get; set; }
        [MaxLength(250)]
        [Display(Name = "اسم القسم")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "القسم الرئيسي")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
