using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface ITagRepository
    {
        /// <summary>
        /// Get a tag in the database based on its Id
        /// </summary>
        /// <param name="id">Tag Id</param>
        /// <returns>Tag if its found. otherwise null</returns>
        public Task<Tag?> GetTagById(int id);
        /// <summary>
        /// Get a tag in the database based on its name
        /// </summary>
        /// <param name="name">Tag name</param>
        /// <returns>Tag if its found. otherwise null</returns>
        public Task<Tag?> GetTagByName(string name);
    }
}