using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.User;

public class RegisterSpecialistDto
{
    [Required(ErrorMessage = "Id is required")]
    public string? Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "Name must have at least 2 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "First last name is required")]
    [MinLength(2, ErrorMessage = "First last name must have at least 2 characters.")]
    public string? FirstLastName { get; set; }

    [Required(ErrorMessage = "Second last name is required")]
    [MinLength(2, ErrorMessage = "Second last name must have at least 2 characters.")]
    public string? SecondLastName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public string? Gender { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = "Phone must have a lenght between 4 and 30 characters.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Speciality Id is required")]
    public int SpecialityId { get; set; }
}