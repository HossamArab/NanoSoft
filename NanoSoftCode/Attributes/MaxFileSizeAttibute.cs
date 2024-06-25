namespace NanoSoftCode.Attributes
{
    public class MaxFileSizeAttibute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttibute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                int MB = (int)file.Length / (1024 * 1024);
                if (MB > _maxFileSize)
                {
                    return new ValidationResult($"اقصي حجم مسموح به {_maxFileSize} ميجا بايت! ");
                }
            }
            return ValidationResult.Success;
        }
    }
}
