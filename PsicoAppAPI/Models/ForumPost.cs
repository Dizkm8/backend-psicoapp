using System.Collections.Generic;

namespace PsicoAppAPI.Models
{
    public class ForumPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateOnly? PublishedOn { get; set; }
        public string? Tag { get; set; }
        public bool IsApproved { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public User User { get; set; } = null!;
    }
}