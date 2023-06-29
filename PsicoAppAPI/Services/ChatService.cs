using PsicoAppAPI.Models.Mobile;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services;

public class ChatService : IChatService
{
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<List<ChatMessage>?> GetListOfMessagesByUserId(string? userId)
    {
        if (userId is null) return null;
        var messages = await _unitOfWork.ChatRepository.GetListOfMessagesByUserId(userId);
        return messages;
    }

    public async Task<ChatMessage?> AddChatMessage(ChatMessage? message)
    {
        if (message is null) return null;
        var addedMessage = await _unitOfWork.ChatRepository.AddChatMessage(message);
        return addedMessage;
    }

    public async Task<List<ChatMessage>?> AddListOfChatMessages(List<ChatMessage>? messages)
    {
        if (messages is null) return null;
        var addedMessages = await _unitOfWork.ChatRepository.AddListOfChatMessages(messages);
        return addedMessages;
    }
}