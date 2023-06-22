using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces;

public interface IForumPostService
{
    /// <summary>
    /// Add a new ForumPost to the database
    /// </summary>
    /// <param name="post">ForumPost to add</param>
    /// <returns>True if could be added. otherwise false</returns>
    public Task<bool> AddForumPost(ForumPost? post);
}