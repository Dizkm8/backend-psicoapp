namespace PsicoAppAPI.DTOs.FeedPost
{
    public class FeedPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateOnly PublishedOn { get; set; } = DateOnly.MinValue;
        public string UserId { get; set; } = null!;
        public int TagId { get; set; }
    }
}