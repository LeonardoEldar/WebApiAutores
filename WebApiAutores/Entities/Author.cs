using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores.Validations;

namespace WebApiAutores.Entities
{
    public class Author : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(maximumLength:32, ErrorMessage = "The field {0} must not have more than {1} characters.")]
        [AuthorValidation]
        public string Name { get; set; }

        // Does not generate the column in DB
        [NotMapped]
        [Range(18,100)]
        public int Age { get; set; }

        [NotMapped]
        [CreditCard]
        public string CreditCard { get; set; }

        [NotMapped]
        [Url]
        public string Url { get; set; }

        public List<Book> Books { get; set; }


        //Validation by model.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name)) {

                string firstLetter = Name[0].ToString();

                if (firstLetter != firstLetter.ToUpper()) {
                    yield return new ValidationResult("The first letter must be capitalized",
                        new string[] { nameof(Name) });
                }
            }
        }
    }
}
