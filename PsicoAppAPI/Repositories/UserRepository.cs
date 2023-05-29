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

    public List<User>? GetUsers() =>
        throw new NotImplementedException();
    // _context.Users?.ToList();

    public User? GetUserById(string id)
    {
        throw new NotImplementedException();
    }

    public User? GetUserByCredentials(string id, string password)
    {
        throw new NotImplementedException();
        // return _context.Users?.FirstOrDefault(x =>
        //     x.Id == id &&
        //     x.Password == password);
    }

    public async Task<Client?> AddClientAndSaveChanges(Client client)
    {
        var addedClient = await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return addedClient.Entity;
    }

    public async Task<Specialist?> AddSpecialistAndSavechanges(Specialist specialist)
    {
        throw new NotImplementedException();
        // var addedSpecialist = await _context.Specialists.AddAsync(specialist);
        // await _context.SaveChangesAsync();
        // return addedSpecialist.Entity;
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

    public async Task<bool> UserExists(string id)
    {
        throw new NotImplementedException();
        // if (id == null) throw new ArgumentNullException(nameof(id));
        // var result = await _context.Users.FindAsync(id) != null;
        // return result;
    }

    public async Task<bool> UserExists(User? user)
    {
        // if (user == null) return false;
        // var result = user.Id != null && await UserExists(user.Id);
        // return result;
        throw new NotImplementedException();
    }
}