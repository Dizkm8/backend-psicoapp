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
    }
}