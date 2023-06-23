using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.ForumPost;

public class AddCommentDto
{
    [Required]
    [StringLength(255, ErrorMessage = "Content cannot be larger than 255 characters.")]
    public string Content { get; set; } = null!;
}