using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces;

public interface IAppointmentStatusesRepository
{
    /// <summary>
    /// Get a AppointmentStatus in the database based on its Id
    /// </summary>
    /// <param name="id">Tag Id</param>
    /// <returns>Tag if its found. otherwise null</returns>
    public Task<AppointmentStatus?> GetStatusById(int id);
    /// <summary>
    /// Get a AppointmentStatus in the database based on its name
    /// </summary>
    /// <param name="name">Tag name</param>
    /// <returns>Tag if its found. otherwise null</returns>
    public Task<AppointmentStatus?> GetStatusByName(string name);
    /// <summary>
    /// Get all AppointmentStatus in the database
    /// </summary>
    /// <returns>IEnumerable with the AppointmentStatus</returns>
    public Task<IEnumerable<AppointmentStatus>?> GetAll();

}