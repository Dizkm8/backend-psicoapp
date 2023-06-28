using Microsoft.EntityFrameworkCore;
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

    public async Task<List<ChatMessage>> GetListOfMessagesByUserId(string userId)
    {
        var messages = await _context.Messages
            .Where(m => m.UserId == userId)
            .Include(m => m.User)
            .ToListAsync();
        return messages;
    }

    public async Task<ChatMessage?> AddChatMessage(ChatMessage message)
    {
        _ = await _context.AddAsync(message);
        var result = await _context.SaveChangesAsync() > 0;
        return result ? message : null;
    }

    public async Task<List<ChatMessage>?> AddListOfChatMessages(List<ChatMessage> messages)
    {
        await _context.AddRangeAsync(messages);
        var result = await _context.SaveChangesAsync() > 0;
        return result ? messages : null;
    }
}