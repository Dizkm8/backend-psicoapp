using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IFeedPostRepository
    {
        /// <summary>
        /// Async FeedPost a new user to the database and save changes
        /// </summary>
        /// <param name="feedPost">FeedPost to add</param>
        /// <returns>True if could be added, false if not</returns>
        public Task<bool> AddFeedPostAndSaveChanges(FeedPost feedPost);
        /// <summary>
        /// Get all FeedPost in the database
        /// </summary>
        /// <returns>List with the FeedPost</returns>
        public Task<List<FeedPost>> GetAllPosts();
        /// <summary>
        /// Get a feed post by their Id
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <returns>FeedPost found. null if do not exists</returns>
        public Task<FeedPost?> GetPostById(int postId);
        /// <summary>
        /// Delete a feed post by their ID
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <returns>true if could be deleted. otherwise false</returns>
        public Task<bool> DeletePostById(int postId);
    }
}