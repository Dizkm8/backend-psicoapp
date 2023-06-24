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
                await _context.Specialists.FirstOrDefaultAsync(specialist => specialist.UserId == userId);
            return specialist;
        }

        public async Task<bool> SpecialistExists(string userId)
        {
            var specialist = await GetSpecialistById(userId);
            return specialist != null;
        }

        public async Task<bool> AddSpecialistAndSaveChanges(Specialist specialist)
        {
            _ = await _context.Specialists.AddAsync(specialist);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Speciality>> GetAllSpecialities()
        {
            var specialities = await _context.Specialities.ToListAsync();
            return specialities;
        }

        public async Task<Speciality?> GetSpecialityById(int specialityId)
        {
            var speciality = await _context.Specialities
                .Where(s => s.Id == specialityId)
                .SingleOrDefaultAsync();
            return speciality;
        }

        public async Task<List<Specialist>?> GetAllUsersSpecialist()
        {
            var users = await _context.Specialists
                .Include(s => s.Speciality)
                .Include(s => s.User)
                .ThenInclude(u => u.Role)
                .ToListAsync();
            return users;
        }
    }
}