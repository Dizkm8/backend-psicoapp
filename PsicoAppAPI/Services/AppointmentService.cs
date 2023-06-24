using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string Booked = "Booked";

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<Appointment>> GetAppointmentsByUser(string userId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAppointmentsByUserOrderDesc(userId);
            return appointments ?? new List<Appointment>();
        }

        public async Task<List<Appointment>> GetAppointmentsBySpecialist(string userId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAppointmentsBySpecialistOrderDesc(userId);
            return appointments ?? new List<Appointment>();
        }

        public async Task<bool> AddAppointment(string requestingUserId, string requestedUserId, DateTime bookedDate)
        {
            var bookedStatusId = (await
                _unitOfWork.AppointmentStatusesRepository.GetAppointmentByName(Booked))?.Id;
            if (bookedStatusId is null) return false;
            var appointment = new Appointment()
            {
                BookedDate = bookedDate,
                RequestingUserId = requestingUserId,
                RequestedUserId = requestedUserId,
                AppointmentStatusId = (int)bookedStatusId // Need to be casted, nullability was already checked 
            };
            var result = await _unitOfWork.AppointmentRepository.AddAppointmentAndSaveChanges(appointment);
            return result;
        }

        public async Task<bool> CancelAppointment(int appointmentId)
        {
            var result = await _unitOfWork.AppointmentRepository.CancelAppointmentAndSaveChanges(appointmentId);
            return result;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            var result = await _unitOfWork.AppointmentRepository.GetAllAppointments();
            return result ?? new List<Appointment>();
        }
    }
}