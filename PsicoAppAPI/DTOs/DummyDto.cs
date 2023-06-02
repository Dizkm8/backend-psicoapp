using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs
{
    public class DummyDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string? Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
    }
}