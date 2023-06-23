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

        public async Task<FeedPost?> GetPostById(int postId)
        {
            var post = await _context.FeedPosts
                .Where(p => p.Id == postId)
                .Include(p => p.User)
                .Include(p => p.Tag)
                .SingleOrDefaultAsync();
            return post;
        }

        public async Task<bool> DeletePostById(int postId)
        {
            // Create a new temp post with the postId to delete correctly from database
            var tempPost = new FeedPost { Id = postId };
            _context.FeedPosts.Remove(tempPost);
            
            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }
    }
}