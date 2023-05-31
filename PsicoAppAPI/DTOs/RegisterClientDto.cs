using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs
{
    public class RegisterClientDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string? Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "First last name is required")]
        public string? FirstLastName { get; set; }
        [Required(ErrorMessage = "Second last name is required")]
        public string? SecondLastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}