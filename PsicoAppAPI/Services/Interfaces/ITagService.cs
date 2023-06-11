using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface ITagService
    {
        /// <summary>
        /// Check if a tag exists in the database based on its name
        /// </summary>
        /// <param name="name">Tag name</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsTagByName(string? name);
        /// <summary>
        /// Check if a tag exists in the database based on its Id
        /// </summary>
        /// <param name="id">Tag Id</param>
        /// <returns>True if exists, otherwise false<</returns>
        public Task<bool> ExistsTagById(int id);
        /// <summary>
        /// Get all tags in the database
        /// </summary>
        /// <returns>IEnumerable with the tags</returns>
        public Task<IEnumerable<Tag>> GetAllTags();
    }
}