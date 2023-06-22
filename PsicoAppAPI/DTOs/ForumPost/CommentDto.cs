namespace PsicoAppAPI.DTOs.ForumPost
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string Username { get; set; } = null!;
        public DateTime PublishedOn { get; set; } = DateTime.MinValue;

    }
}