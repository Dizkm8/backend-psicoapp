using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class FeedPostRepository : IFeedPostRepository
    {
        private readonly DataContext _context;

        public FeedPostRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFeedPostAndSaveChanges(FeedPost feedPost)
        {
            await _context.FeedPosts.AddAsync(feedPost);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<List<FeedPost>> GetAllPosts()
        {
            var posts = await _context.FeedPosts
                .Include(p => p.User)
                .Include(p => p.Tag)
                .ToListAsync();
            return posts;
        }
    }
}