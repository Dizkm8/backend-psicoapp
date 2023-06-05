using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.Validations
{
    public class MinutesSecondsEqualsZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime startTime)
            {
                if (startTime.Minute == 0 && startTime.Second == 0 &&
                    startTime.Microsecond == 0 && startTime.Millisecond == 0)
                    return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}