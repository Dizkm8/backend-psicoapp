using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsicoAppAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<Appointment>> GetAppointmentsByUser(int userId)
        {
            return await _appointmentRepository.GetAppointmentsByUser(userId);
        }

        public Task<bool> AddAppointment(string RequestingUserId, string RequestedUserId, DateTime BookedDate)
        {
            throw new NotImplementedException();
        }
    }
}
