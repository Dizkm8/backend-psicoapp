using PsicoAppAPI.DTOs.BasePosts;

namespace PsicoAppAPI.Mediators.Interfaces
{
    public interface ITagManagementService
    {
        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>IEnumerable with tags</returns>
        public Task<IEnumerable<TagDto>> GetTags();
    }
}