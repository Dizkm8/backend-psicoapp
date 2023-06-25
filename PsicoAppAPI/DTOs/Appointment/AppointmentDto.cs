using PsicoAppAPI.Models;

namespace PsicoAppAPI.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime BookedDate { get; set; }
        public string AppointmentStatusName { get; set; } = null!;
        public int AppointmentStatusId { get; set; }
    }
}