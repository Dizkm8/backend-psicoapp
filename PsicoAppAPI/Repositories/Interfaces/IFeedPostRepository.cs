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
    }
}