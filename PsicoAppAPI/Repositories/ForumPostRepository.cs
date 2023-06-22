using PsicoAppAPI.Data;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories;

public class ForumPostRepository : IForumPostRepository
{
    private readonly DataContext _context;

    public ForumPostRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}