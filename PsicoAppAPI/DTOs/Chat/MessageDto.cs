namespace PsicoAppAPI.DTOs.Chat;

public class MessageDto
{
    public string Content { get; set; } = null!;
    public DateTime SendOn { get; set; }
    public bool IsBotAnswer { get; set; }
}