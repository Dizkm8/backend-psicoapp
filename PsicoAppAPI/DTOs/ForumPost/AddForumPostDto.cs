using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.ForumPost;

public class AddForumPostDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot be larger than 200 characters.")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Content is required")]
    [StringLength(2500, ErrorMessage = "Content cannot be larger than 2500 characters.")]
    public string Content { get; set; } = null!;

    [Required(ErrorMessage = "TagId is required")]
    public int TagId { get; set; }
}