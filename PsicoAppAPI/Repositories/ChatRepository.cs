using PsicoAppAPI.Data;
using PsicoAppAPI.Models.Mobile;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly DataContext _context;

    public ChatRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<ChatMessage>?> GetListOfMessagesByUserId(string userId)
    {
        throw new NotImplementedException();
    }
}