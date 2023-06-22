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
}