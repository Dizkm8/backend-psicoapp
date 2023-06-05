using System.ComponentModel.DataAnnotations;
using PsicoAppAPI.DTOs.Validations;

namespace PsicoAppAPI.DTOs.Specialist
{
    public class AddAvailabilityDto
    {
        [Required(ErrorMessage = "StartTime is required")]
        [CurrentDateOrLater(ErrorMessage = "StartTime must be equal to or later than the current date")]
        public DateTime StartTime { get; set; }
    }
}