using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs
{
    public class UpdateProfileInformationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2,ErrorMessage = "Name must have at least 2 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "First last name is required")]
        [MinLength(2,ErrorMessage = "First last name must have at least 2 characters.")]
        public string? FirstLastName { get; set; }

        [Required(ErrorMessage = "Second last name is required")]
        [MinLength(2,ErrorMessage = "Second last name must have at least 2 characters.")]
        public string? SecondLastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Range(10000000, 99999999, ErrorMessage = "Phone number must be an 8-digit number.")]
        public int Phone { get; set; }
    }
}