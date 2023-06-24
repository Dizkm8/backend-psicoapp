using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DataContext _context;

        public AppointmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>?> GetAppointmentsByUser(string userId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.RequestingUserId == userId)
                .Include(a => a.RequestingUser)
                .Include(a => a.RequestedUser)
                .Include(a => a.AppointmentStatus)
                .ToListAsync();
            return appointments;
        }

        public async Task<bool> AddAppointmentAndSaveChanges(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}