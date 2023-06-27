using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.UpdateProfileInformation
{
    public class UpdateProfileInformationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2,ErrorMessage = "Name must have at least 2 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "First last name is required")]
        [MinLength(2,ErrorMessage = "First last name must have at least 2 characters.")]
        public string FirstLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Second last name is required")]
        [MinLength(2,ErrorMessage = "Second last name must have at least 2 characters.")]
        public string SecondLastName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = string.Empty;
    }
}