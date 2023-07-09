using PsicoAppAPI.DTOs.FeedPost;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IFeedPostService
    {
        /// <summary>
        /// Add a new FeedPost to the database
        /// </summary>
        /// <param name="feedPost">Feedpost to add</param>
        /// <returns>True if could be added. otherwise false</returns>
        public Task<bool> AddFeedPost(FeedPost? feedPost);
        /// <summary>
        /// Get all FeedPost from the database
        /// </summary>
        /// <returns>IEnumerable with the FeedPost</returns>
        public Task<List<FeedPost>> GetAllPosts();
        /// <summary>
        /// Get a post by their postId
        /// </summary>
        /// <returns>Post if exists. otherwise null</returns>
        public Task<FeedPost?> GetPostById(int postId);
        /// <summary>
        /// Delete a post by their postId
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <returns>true if the post could be deleted. otherwise false</returns>
        public Task<bool> DeletePostById(int postId);
    }
}