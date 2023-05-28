using PsicoAppAPI.Data;
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

    public User GetUserById(string id)
    {
        throw new NotImplementedException();
    }

    public User? GetUserByCredentials(string id, string password)
    {
        return _context.Users?.FirstOrDefault(x =>
            x.Id == id &&
            x.Password == password);
    }

    public User AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public User UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public bool UserExists(string id)
    {
        if(id == null) throw new ArgumentNullException(nameof(id));
        if(_context.Users == null) throw new ArgumentNullException(nameof(_context.Users));
        return _context.Users.Any(e => e.Id == id);
    }
}