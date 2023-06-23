using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces;

public interface ICommentRepository
{
    /// <summary>
    /// Async add a new Comment to the database
    /// </summary>
    /// <param name="comment">Comment to add</param>
    /// <returns>True if could be added, false if not</returns>
    public Task<bool> AddCommentAndSaveChanges(Comment comment);
}