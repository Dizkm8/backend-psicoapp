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
        ///  Add a Specialist to database
        /// </summary>
        /// <param name="specialist">Specialist to add</param>
        /// <returns>True if could be added</returns>
        public bool AddSpecialist(Specialist specialist);
        /// <summary>
        /// Asynchronous add a Specialist to database and save the changes
        /// </summary>
        /// <param name="specialist">Specialist to add</param>
        /// <returns>True if could be added</returns>
        public Task<bool> AddSpecialistAndSaveChanges(Specialist specialist);
        /// <summary>
        /// Save the changes in the database
        /// </summary>
        /// <returns>True if any change was made</returns>
        public bool SaveChanges();
        /// <summary>
        /// Asynchronous save the changes in the database
        /// </summary>
        /// <returns>True if any change was made</returns>
        public Task<bool> SaveChangesAsync();
        /// <summary>
        /// Check if a Specialist exists in the database by the userId attached to it
        /// </summary>
        /// <param name="userId">Respective userId in the Specialist entity</param>
        /// <returns>True if exists</returns>
        public Task<bool> SpecialistExists(string userId);
        /// <summary>
        /// Check if a Specialist exists in the database extracting their userId attached to it
        /// </summary>
        /// <param name="specialist">Specialist to extract userId</param>
        /// <returns>True if exists</returns>
        public Task<bool> SpecialistExists(Specialist specialist);
    }
}