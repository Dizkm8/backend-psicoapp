using PsicoAppAPI.Models.Mobile;

namespace PsicoAppAPI.Services.Interfaces;

public interface IChatService
{
    /// <summary>
    /// Get all the messages sent or received by user
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <returns>List with chats. empty if have no messages. null if id is null</returns>
    public Task<List<ChatMessage>?> GetListOfMessagesByUserId(string? userId);

    /// <summary>
    /// Add a new chat message to the system
    /// </summary>
    /// <param name="message">Chat message with the info</param>
    /// <returns>Message if could added. otherwise null</returns>
    public Task<ChatMessage?> AddChatMessage(ChatMessage? message);

    /// <summary>
    /// Add a new list of messages to the system
    /// </summary>
    /// <param name="messages">List of messages</param>
    /// <returns>List with the added messages. null if cannot be added</returns>
    public Task<List<ChatMessage>?> AddListOfChatMessages(List<ChatMessage>? messages);
}