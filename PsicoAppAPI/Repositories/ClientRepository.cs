using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;
         public ClientRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddClientAndSaveChanges(Client client)
        {
            await _context.Clients.AddAsync(client);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> ClientExists(string id)
        {
            var client = await _context.FindAsync<Client>(id);
            return client != null;
        }

        public async Task<bool> ClientExists(Client client)
        {
            if(client.Id == null) return false;
            var result = await this.ClientExists(client.Id);
            return result;
        }

        public Task<Client?> GetClientByCredentials(string id, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Client?> GetClientById(string id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}