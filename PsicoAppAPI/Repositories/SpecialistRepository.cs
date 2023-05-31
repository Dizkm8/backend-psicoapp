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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool AddSpecialist(Specialist specialist)
        {
            return _context.Specialists.Add(specialist) != null;
        }

        public async Task<bool> AddSpecialistAndSaveChanges(Specialist specialist)
        {
            await _context.Specialists.AddAsync(specialist);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<Specialist?> GetSpecialistById(string userId)
        {
            var specialist =
                await _context.Specialists.
                FirstOrDefaultAsync(specialist => specialist.UserId == userId);
            return specialist;
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

        public async Task<bool> SpecialistExists(string userId)
        {
            var specialist = await GetSpecialistById(userId);
            return specialist != null;

        }

        public async Task<bool> SpecialistExists(Specialist specialist)
        {
            if (specialist.UserId == null) return false;
            var result = await SpecialistExists(specialist.UserId);
            return result;
        }
    }
}