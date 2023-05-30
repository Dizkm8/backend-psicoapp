using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IClientRepository
    {   
        /// <summary>
        /// Asynchronously get a Client by the userId attached to it
        /// </summary>
        /// <param name="userId">Respective userId in the client entity</param>
        /// <returns>Client if was found, null if not</returns>
        public Task<Client?> GetClientById(string userId);
        /// <summary>
        ///  Add a Client to database
        /// </summary>
        /// <param name="Client">Client to add</param>
        /// <returns>True if could be added</returns>
        public bool AddClient(Client Client);
        /// <summary>
        /// Asynchronous add a Client to database and save the changes
        /// </summary>
        /// <param name="Client">Client to add</param>
        /// <returns>True if could be added</returns>
        public Task<bool> AddClientAndSaveChanges(Client client);
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
        /// Check if a Client exists in the database by the userId attached to it
        /// </summary>
        /// <param name="userId">Respective userId in the client entity</param>
        /// <returns>True if exists</returns>
        public Task<bool> ClientExists(string userId);
        /// <summary>
        /// Check if a Client exists in the database extracting their userId attached to it
        /// </summary>
        /// <param name="Client">Client to extract userId</param>
        /// <returns>True if exists</returns>
        public Task<bool> ClientExists(Client client);
    }
}