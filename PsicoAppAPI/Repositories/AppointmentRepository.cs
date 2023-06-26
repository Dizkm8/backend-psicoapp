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

        public async Task<List<Appointment>?> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.RequestingUser)
                .Include(a => a.RequestedUser)
                .Include(a => a.AppointmentStatus)
                .ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>?> GetAppointmentsByClient(string userId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.RequestingUserId == userId)
                .Include(a => a.RequestingUser)
                .Include(a => a.RequestedUser)
                .Include(a => a.AppointmentStatus)
                .ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>?> GetAppointmentsByClientOrderDesc(string userId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.RequestingUserId == userId)
                .Include(a => a.RequestingUser)
                .Include(a => a.RequestedUser)
                .Include(a => a.AppointmentStatus)
                .OrderByDescending(a => a.BookedDate)
                .ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>?> GetAppointmentsBySpecialistOrderDesc(string userId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.RequestedUserId == userId)
                .Include(a => a.RequestingUser)
                .Include(a => a.RequestedUser)
                .Include(a => a.AppointmentStatus)
                .OrderByDescending(a => a.BookedDate)
                .ToListAsync();
            return appointments;
        }

        public async Task<bool> AddAppointmentAndSaveChanges(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CancelAppointmentAndSaveChanges(int appointmentId)
        {
            var appointment = await _context.Appointments
                .Where(a => a.Id == appointmentId)
                .Include(a => a.RequestingUser)
                .Include(a => a.RequestedUser)
                .Include(a => a.AppointmentStatus)
                .SingleOrDefaultAsync();
            if (appointment is null) return false;
            // Get the appointment status entity of canceled
            var newStatus = await _context.AppointmentStatuses.Where(a => a.Name.ToLower() == "canceled")
                .SingleOrDefaultAsync();
            if (newStatus is null) return false;
            // Set the new appointment status to canceled
            appointment.AppointmentStatus = newStatus;
            appointment.AppointmentStatusId = newStatus.Id;

            // Set the availability of the specialist to isAvailable = true
            var availability = await _context.AvailabilitySlots
                .Where(a => a.UserId == appointment.RequestedUserId && a.StartTime == appointment.BookedDate)
                .SingleOrDefaultAsync();
            if (availability is null) return false;
            availability.IsAvailableOverride = true;

            // finally update the appointment
            _context.Appointments.Update(appointment);
            _context.AvailabilitySlots.Update(availability);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetDoneAppointmentsQuantity(int statusId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.AppointmentStatusId == statusId)
                .ToListAsync();
            return appointments.Count;
        }
    }
}