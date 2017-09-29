using System.ComponentModel.DataAnnotations;

namespace ArchitectureTemplate.Mvc.Models.Shared
{
    public class ValidationAttributeExtention : ValidationAttribute
    {
        private readonly int _espaco;
        public ValidationAttributeExtention(int espaco)
            : base(@"{0} Contém espaços além do permitido")
        {
            _espaco = espaco;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueAsSting = value?.ToString();
            if (valueAsSting?.Split(' ').Length > _espaco)
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}