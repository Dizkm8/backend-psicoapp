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
        /// <summary>
        /// Add a specialist to the database and save changes
        /// </summary>
        /// <param name="specialist">Specialist to add</param>
        /// <returns>true if could be added. otherwise false</returns>
        public Task<bool> AddSpecialistAndSaveChanges(Specialist specialist);
        /// <summary>
        /// Get all the specialities of Specialist in the system
        /// </summary>
        /// <returns>List of Speciality</returns>
        public Task<List<Speciality>> GetAllSpecialities();
        /// <summary>
        /// Get a speciality by their Id
        /// </summary>
        /// <param name="specialityId">Id of the speciality</param>
        /// <returns>Speciality if could be found. otherwise null</returns>
        public Task<Speciality?> GetSpecialityById(int specialityId);
    }
}