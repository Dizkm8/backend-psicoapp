using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Data
{
    public class PsicoAppContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<FeedPost>? FeedPosts { get; set; }
        // public DbSet<FeedPost>? Comment { get; set; }

        public PsicoAppContext(DbContextOptions options) : base(options)
        {
        }

    }
}