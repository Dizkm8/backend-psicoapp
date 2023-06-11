using System.ComponentModel.DataAnnotations;
using PsicoAppAPI.Util;

namespace PsicoAppAPI.DTOs.Validations
{
    public class DateWithinEightWeeksAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime startTime)
            {
                var date = DateOnly.FromDateTime(startTime);
                if (DateHelper.DateIsBetweenNowAndSpecificWeek(date, 8))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}