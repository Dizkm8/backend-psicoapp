using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface ISpecialistRepository
    {
        /// <summary>
        /// Asynchronously get a specialists searching into users using the userId attached to it
        /// </summary>
        /// <param name="userId">Respective userId in the Specialist entity</param>
        /// <returns>Specialist if was found, null if not</returns>
        public Task<Specialist?> GetSpecialistById(string userId);
        /// <summary>
        /// Check if a Specialist exists in the database by the userId attached to it
        /// </summary>
        /// <param name="userId">Respective userId in the Specialist entity</param>
        /// <returns>True if exists</returns>
        public Task<bool> SpecialistExists(string userId);
    }
}