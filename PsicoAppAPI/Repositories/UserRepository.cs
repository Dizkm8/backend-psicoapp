using PsicoAppAPI.Data;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public List<User>? GetUsers() => _context.Users?.ToList();

    public User? GetUserById(string id)
    {
        throw new NotImplementedException();
    }

    public User? GetUserByCredentials(string id, string password)
    {
        return _context.Users?.FirstOrDefault(x =>
            x.Id == id &&
            x.Password == password);
    }

    public async Task<Client?> AddClientAndSaveChanges(Client client)
    {
        var addedClient = await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return addedClient.Entity;
    }

    public async Task<Specialist?> AddSpecialistAndSavechanges(Specialist specialist)
    {
        var addedSpecialist = await _context.Specialists.AddAsync(specialist);
        await _context.SaveChangesAsync();
        return addedSpecialist.Entity;
    }

    public User? UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }

    public bool UserExists(string id)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));
        return _context.Users.Any(e => e.Id == id);
    }

    public bool UserExists(User? user)
    {
        if (user == null) return false;
        return user.Id != null && UserExists(user.Id);
    }
}