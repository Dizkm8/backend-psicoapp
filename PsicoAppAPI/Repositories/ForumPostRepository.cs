using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories;

public class ForumPostRepository : IForumPostRepository
{
    private readonly DataContext _context;

    public ForumPostRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> AddForumPostAndSaveChanges(ForumPost post)
    {
        await _context.ForumPosts.AddAsync(post);
        var result = await _context.SaveChangesAsync() > 0;
        return result;
    }

    public async Task<List<ForumPost>> GetAllPosts()
    {
        var posts = await _context.ForumPosts.Include(p => p.User)
            .Include(p => p.Tag)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .ToListAsync();
        return posts;
    }

    public async Task<ForumPost?> GetPostById(int postId)
    {
        var post = await _context.ForumPosts
            .Where(p => p.Id == postId)
            .Include(p => p.User)
            .Include(p => p.Tag)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .SingleOrDefaultAsync();
        return post;
    }

    public async Task<bool> ExistsPost(int postId)
    {
        var post = await GetPostById(postId);
        return post is not null;
    }

    public async Task<bool> DeletePostById(int postId)
    {
        var post = await _context.ForumPosts.SingleOrDefaultAsync(p => p.Id == postId);
        if (post is null) return false;
        _context.Remove(post);

        var result = await _context.SaveChangesAsync() > 0;
        return result;
    }

    public async Task<bool> DeleteCommentByIdAndPostId(int postId, int commentId)
    {
        var post = await _context.ForumPosts.SingleOrDefaultAsync(p => p.Id == postId);

        var comment = post?.Comments.SingleOrDefault(c => c.Id == commentId);
        if (comment is null) return false;
        
        _context.Remove(comment);
        var result = await _context.SaveChangesAsync() > 0;
        return result;
    }
}