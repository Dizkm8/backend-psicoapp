using PsicoAppAPI.DTOs.ForumPost;
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
    /// <summary>
    /// Get all ForumPosts from the database
    /// </summary>
    /// <returns>IEnumerable with the forum posts</returns>
    public Task<IEnumerable<ForumPostDto>> GetAllPosts();
}