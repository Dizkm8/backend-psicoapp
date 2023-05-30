using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool AddUser(User user)
        {
            return _context.Users.Add(user) != null;
        }

        public async Task<bool> AddUserAndSaveChanges(User user)
        {
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<User?> GetUserByCredentials(string id, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user =>
                user.Id == id && user.Password == password);
            return user;
        }

        public async Task<User?> GetUserById(string id)
        {
            var result = await _context.FindAsync<User>(id);
            return result;
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

        public async Task<bool> UserExists(string id)
        {
            var user = await _context.FindAsync<User>(id);
            return user != null;

        }

        public async Task<bool> UserExists(User user)
        {
            if (user.Id == null) return false;
            var result = await this.UserExists(user.Id);
            return result;
        }
    }
}