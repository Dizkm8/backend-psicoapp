using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DataContext _context;

    public CommentRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> AddCommentAndSaveChanges(Comment comment)
    {
        _ = await _context.Comments.AddAsync(comment);
        var result = await _context.SaveChangesAsync() > 0;
        return result;
    }
}