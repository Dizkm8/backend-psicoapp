using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories;

public class SpecialitiesRepository : ISpecialitiesRepository
{
    private readonly DataContext _context;

    public SpecialitiesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Speciality?> GetSpecialityById(int id)
    {
        var speciality = await _context.Specialities.FindAsync(id);
        return speciality;
    }
}
