using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class SpecialistRepository : ISpecialistRepository
    {
        private readonly DataContext _context;

        public SpecialistRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Specialist?> GetSpecialistById(string userId)
        {
            var specialist =
                await _context.Specialists.
                FirstOrDefaultAsync(specialist => specialist.UserId == userId);
            return specialist;
        }

        public async Task<bool> SpecialistExists(string userId)
        {
            var specialist = await GetSpecialistById(userId);
            return specialist != null;
        }
    }
}