using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsicoAppAPI.Models
{
    public class FeedPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateOnly? OnPublished { get; set; }
        public string? Tag { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public User User { get; set; } = null!;
    }
}