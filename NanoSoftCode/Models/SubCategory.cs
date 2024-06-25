namespace NanoSoftCode.Models
{
    public class SubCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product>? Product { get; set; }
    }
}
