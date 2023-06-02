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

        public bool AddClient(Client client)
        {
            return _context.Clients.Add(client) != null;
        }

        public async Task<bool> AddClientAndSaveChanges(Client client)
        {
            await _context.Clients.AddAsync(client);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<Client?> GetClientById(string userId)
        {
            var client =
                await _context.Clients.
                FirstOrDefaultAsync(client => client.UserId == userId);
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

        public async Task<bool> ClientExists(string userId)
        {
            var client = await GetClientById(userId);
            return client != null;

        }

        public async Task<bool> ClientExists(Client client)
        {
            if (client.UserId == null) return false;
            var result = await ClientExists(client.UserId);
            return result;
        }

        public Client CreateClient(bool isAdministrator, string userId)
        {
            return new Client
            {
                IsAdministrator = isAdministrator,
                UserId = userId
            };
        }
    }
}