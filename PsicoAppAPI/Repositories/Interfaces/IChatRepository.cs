using PsicoAppAPI.Models.Mobile;

namespace PsicoAppAPI.Repositories.Interfaces;

public interface IChatRepository
{
    /// <summary>
    /// Get all the messages sent or received by user
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <returns>List with chats. null if userId do not exists</returns>
    public Task<List<Chat>?> GetListOfMessagesByUserId(string userId);
}