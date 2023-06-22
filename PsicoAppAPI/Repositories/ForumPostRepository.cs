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
                                             .ToListAsync();
        return posts;
    }


}