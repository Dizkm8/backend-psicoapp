using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IClientRepository
    {
        /// <summary>
        /// Asynchronously get a client by their Id
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>The client if it's found, null if not</returns>
        public Task<Client?> GetClientById(string id);

        /// <summary>
        /// Asynchronously get a client by their credentials 
        /// </summary>
        /// <param name="id">Client Id</param>
        /// <param name="password">Client password</param>
        /// <returns>The client if it's found, null if not</returns>
        public Task<Client?> GetClientByCredentials(string id, string password);
        /// <summary>
        /// Asynchronous add a client to database and save the changes
        /// </summary>
        /// <param name="client">Client to add</param>
        /// <returns>True if could be added</returns>
        public Task<bool> AddClientAndSaveChanges(Client client);
        /// <summary>
        /// Save the changes in the database
        /// </summary>
        /// <returns>True if any change was made</returns>
        public bool SaveChanges();
        /// <summary>
        /// Check if a client exists in the database
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>True if exists</returns>
        public Task<bool> ClientExists(string id);
        /// <summary>
        /// Check if a client exists in the database by their Id
        /// </summary>
        /// <param name="client">Client to get Id</param>
        /// <returns>True if exists</returns>
        public Task<bool> ClientExists(Client client);
    }
}