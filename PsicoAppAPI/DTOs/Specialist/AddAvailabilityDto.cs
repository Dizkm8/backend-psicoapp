using System.ComponentModel.DataAnnotations;
using PsicoAppAPI.DTOs.Validations;

namespace PsicoAppAPI.DTOs.Specialist
{
    public class AddAvailabilityDto
    {
        [Required(ErrorMessage = "StartTime is required")]
        [DateWithinEightWeeks(ErrorMessage = "StartTime must be equal to or less than 8 weeks from monday's current week")]
        [MinutesSecondsEqualsZero(
            ErrorMessage = "StartTime cannot have minutes, seconds, milliseconds or microseconds different from 0")]
        public DateTime StartTime { get; set; }
    }
}