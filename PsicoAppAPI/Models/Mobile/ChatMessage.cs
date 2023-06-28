using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models.Mobile;

public class ChatMessage
{
    [Key] public int Id { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public string Content { get; set; } = null!;
    public DateTime SendOn { get; set; }
    public bool IsBotAnswer { get; set; }
}