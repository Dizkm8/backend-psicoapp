using PsicoAppAPI.Models;

namespace PsicoAppAPI.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime BookedDate { get; set; }
        public string RequestedUserId { get; set; } = null!;
        public string RequestedUserName { get; set; } = null!;
        public string RequestedUserFirstLastName { get; set; } = null!;
        public string RequestedUserSecondLastName { get; set; } = null!;

        public string RequestedUserFullName =>
            $"{RequestedUserName} {RequestedUserFirstLastName} {RequestedUserSecondLastName}";

        public string AppointmentStatusName { get; set; } = null!;
    }
}