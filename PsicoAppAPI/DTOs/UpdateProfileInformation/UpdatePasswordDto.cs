using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.UpdateProfileInformation
{
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "Current password is required")]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Password must have a length between 10 and 15 characters.")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new Password is required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Password must have a length between 10 and 15 characters.")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmNewPassword { get; set; }
    }
}