using System.ComponentModel.DataAnnotations;
using PsicoAppAPI.DTOs.Validations;

namespace PsicoAppAPI.DTOs.Specialist
{
    public class AddAvailabilityDto
    {
        [Required(ErrorMessage = "StartTime is required")]
        [MinutesSecondsEqualsZero(
            ErrorMessage = "StartTime cannot have minutes, seconds, milliseconds or microseconds different from 0")]
        public DateTime StartTime { get; set; }
    }
}