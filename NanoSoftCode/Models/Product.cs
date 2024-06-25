namespace NanoSoftCode.Models
{
    public class Product : BaseEntity
    {
        [MaxLength(2500)]
        public string? Description { get; set; }
        [MaxLength(500)]
        public string? ImagePath { get; set; }
        [MaxLength(500)]
        public string? Datasheet { get; set; }
        [MaxLength(500)]
        public string? Driver { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? SubCategoryId { get; set; } = 0;
        public SubCategory? SubCategory { get; set; }
    }
}
