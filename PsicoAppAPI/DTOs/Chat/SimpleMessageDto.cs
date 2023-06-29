using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.Chat;

public class SimpleMessageDto
{
    [MaxLength(500, ErrorMessage = "The content cannot be larger than 500 characters")]
    public string Content { get; set; } = null!;
}