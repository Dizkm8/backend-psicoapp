using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces;

public interface ISpecialitiesRepository
{
    /// <summary>
    /// Get a speciality by their Id
    /// </summary>
    /// <param name="Id">Id to search</param>
    /// <returns>Speciality found. null if dont exists</returns>
    public Task<Speciality?> GetSpecialityById(int id);
}
