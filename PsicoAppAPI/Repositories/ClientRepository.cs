using Microsoft.EntityFrameworkCore;
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

        public bool AddClient(Client client)
        {
            return _context.Clients.Add(client) != null;
        }

        public async Task<bool> ClientExists(string id)
        {
            var client = await _context.FindAsync<Client>(id);
            return client != null;
        }

        public async Task<bool> ClientExists(Client client)
        {
            if (client.Id == null) return false;
            var result = await this.ClientExists(client.Id);
            return result;
        }

        public async Task<Client?> GetClientById(string id)
        {
            var result = await _context.FindAsync<Client>(id);
            return result;
        }

        public async Task<Client?> GetClientByCredentials(string id, string password)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(client =>
                client.Id == id && client.Password == password);
            return client;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }


    }
}