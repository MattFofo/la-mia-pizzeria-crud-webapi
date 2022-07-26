using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.ValidatorAttributes
{
    public class NoMoreThanFiveWordsValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;

            if (fieldValue == null)
            {
                return new ValidationResult(ErrorMessage = "Il campo non può essere vuoto!");

            } else
            {
                string[] parts = fieldValue
                .Split(' ')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToArray();


                if (parts.Length < 5)
                {
                    return new ValidationResult(ErrorMessage = "Il campo deve contenere almeno 5 parole!");

                } else
                {

                    return ValidationResult.Success;
                }
            }
        }
    }
}
