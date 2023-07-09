using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.ForumPost;

public class AddCommentDto
{
    [Required]
    [StringLength(2500, ErrorMessage = "Content cannot be larger than 2500 characters.")]
    public string Content { get; set; } = null!;
}