using System.ComponentModel.DataAnnotations;

namespace damage_assessment_api.Shared
{
    public class ExtensionValidatorAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public ExtensionValidatorAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult($"Only {string.Join(", ", _extensions)} files are allowed.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
