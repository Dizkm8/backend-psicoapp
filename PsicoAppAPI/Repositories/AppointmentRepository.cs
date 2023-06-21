using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsicoAppAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DataContext _context;

        public AppointmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAppointmentsByUser(int userId)
        {
            var userIdString = userId.ToString();
            return await _context.Appointments
                .Include(a => a.RequestingUser)
                .Include(a => a.RequestedUser)
                .Include(a => a.AppointmentStatus)
                .Where(a => a.RequestingUserId == userIdString || a.RequestedUserId == userIdString)
                .ToListAsync();
        }

        public async Task<bool> AddAppointmentAndSaveChanges(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
