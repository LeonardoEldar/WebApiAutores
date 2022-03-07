using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Validations
{
    //Custom validation by entity attribute.
    public class AuthorValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) {
                return ValidationResult.Success;
            }

            string firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("The first letter must be capitalized");
            }

            return ValidationResult.Success;
        }
    }
}
