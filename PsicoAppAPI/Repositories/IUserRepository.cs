using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories;

public interface IUserRepository
{
    public List<User>? GetUsers();
    public User? GetUserById(string id);
    public User? GetUserByCredentials(string id, string password);
    public User? AddUser(User user);
    public User? UpdateUser(User user);
    public void DeleteUser(User user);
    public bool SaveChanges();
    
    public bool UserExists(string id);
}