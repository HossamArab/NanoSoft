
namespace NanoSoftCode.Models
{
    public class Category : BaseEntity
    {
        public string? ImagePath { get; set; }
        public ICollection<SubCategory>? SubCategory { get; set; }
        public ICollection<Product>? Product { get; set; }
    }
}
