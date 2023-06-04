using PsicoAppAPI.DTOs.FeedPost;

namespace PsicoAppAPI.Services.Mediators.Interfaces
{
    public interface IFeedPostManagementService
    {
        /// <summary>
        /// Add a new FeedPost to the database
        /// </summary>
        /// <param name="feedPostDto">FeedpostDto shape to add</param>
        /// <returns>True if could be added. otherwise false</returns>
        public Task<bool> AddFeedPost(AddFeedPostDto? feedPostDto);
    }
}