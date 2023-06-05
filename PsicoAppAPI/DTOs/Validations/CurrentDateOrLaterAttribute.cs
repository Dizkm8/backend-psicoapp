using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.Validations
{
    public class CurrentDateOrLaterAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? startTime = (DateTime?)value;
            DateTime currentDate = DateTime.Now;

            if (startTime != null && startTime.Value.Date < currentDate.Date)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}