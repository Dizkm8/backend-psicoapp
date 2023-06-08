using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.Validations
{
    public class TimeWithinEightAndTwentyHourAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime time)
            {
                var maxTime = new TimeSpan(20, 0, 0);
                var minTime = new TimeSpan(8, 0, 0);
                if (time.TimeOfDay >= minTime && time.TimeOfDay <= maxTime) return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}