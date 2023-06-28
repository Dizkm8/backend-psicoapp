using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.Chat;

public class SimpleMessageDto
{
    [MaxLength(255, ErrorMessage = "The content cannot be larger than 255 characters")]
    public string Content { get; set; } = null!;
}