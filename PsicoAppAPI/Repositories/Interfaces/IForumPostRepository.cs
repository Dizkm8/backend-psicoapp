using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces;

public interface IForumPostRepository
{
    /// <summary>
    /// Async ForumPost a new user to the database and save changes
    /// </summary>
    /// <param name="post">ForumPost to add</param>
    /// <returns>True if could be added, false if not</returns>
    public Task<bool> AddForumPostAndSaveChanges(ForumPost post);
}