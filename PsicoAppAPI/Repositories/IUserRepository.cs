using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Get all users in the database
    /// </summary>
    /// <returns></returns>
    public List<User>? GetUsers();
    /// <summary>
    /// Get a user by its id
    /// </summary>
    /// <param name="id">Id of the user</param>
    /// <returns>User found, null if not exists</returns>
    public User? GetUserById(string id);
    /// <summary>
    /// Get user by their credentials
    /// </summary>
    /// <param name="id"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public User? GetUserByCredentials(string id, string password);
    /// <summary>
    /// Asynchronous add a user to the database
    /// </summary>
    /// <param name="user">User to add</param>
    /// <returns>Added user</returns>
    public Task<User?> AddUser(User? user);
    
    /// <summary>
    /// Asynchronous add a user to database and save the changes
    /// </summary>
    /// <param name="user">User to add</param>
    /// <returns>Added user</returns>
    public Task<User?> AddUserAndSaveChanges(User user);
    
    public User? UpdateUser(User user);
    public void DeleteUser(User user);
    public bool SaveChanges();
    
    /// <summary>
    /// check if a user exists with the same Id
    /// </summary>
    /// <param name="id">User Id to check</param>
    /// <returns>True if exists</returns>
    public bool UserExists(string id);
    /// <summary>
    /// Check if a user exists in the database extracting their Id
    /// </summary>
    /// <param name="user">User to check</param>
    /// <returns>True if exists</returns>
    public bool UserExists(User? user);
}