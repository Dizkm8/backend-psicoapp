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
    public Task<List<ForumPost>> GetAllPosts();

    /// <summary>
    /// Check if a post exists based on their post Id
    /// </summary>
    /// <param name="postId">Id of the post</param>
    /// <returns>true if exists. otherwise false</returns>
    public Task<bool> ExistsPost(int postId);
    /// <summary>
    /// Add a new comment to a post identified by their post Id
    /// </summary>
    /// <param name="comment">Comment to add</param>
    /// <param name="postId">post Id to add comment</param>
    /// <returns>The Comment if could be added. otherwise null</returns>
    public Task<Comment?> AddComment(Comment comment, int postId);
}