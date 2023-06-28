using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.Chat;

public class SendMessageDto
{
    [MaxLength(255, ErrorMessage = "The content cannot be larger than 255 characters")]
    public string Content { get; set; } = null!;
}