using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.DTOs.FeedPost
{
    public class AddFeedPostDto 
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Content cannot be larger than 200 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Content is required")]
        [StringLength(2500, ErrorMessage = "Content cannot be larger than 2500 characters.")]
        public string Content { get; set; } = null!;

        [Required(ErrorMessage = "TagId is required")]
        public int TagId { get; set; }
    }
}