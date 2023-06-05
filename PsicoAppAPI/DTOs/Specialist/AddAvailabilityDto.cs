using System.ComponentModel.DataAnnotations;
using PsicoAppAPI.DTOs.Validations;

namespace PsicoAppAPI.DTOs.Specialist
{
    public class AddAvailabilityDto
    {
        [Required(ErrorMessage = "StartTime is required")]
        [CurrentDateOrLater(ErrorMessage = "StartTime must be equal to or later than the current date")]
        [DateWithinEightWeeks(ErrorMessage = "StartTime must be equal to or less than 8 weeks from monday's current week")]
        [TimeWithinEightAndTwentyHour(ErrorMessage = "StartTime must be between 8:00 and 20:00")]
        [MinutesSecondsEqualsZero(ErrorMessage = "StartTime must have 0 minutes and 0 seconds")]
        public DateTime StartTime { get; set; }
    }
}