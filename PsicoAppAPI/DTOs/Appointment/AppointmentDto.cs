using PsicoAppAPI.Models;

namespace PsicoAppAPI.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime BookedDate { get; set; }
        public string RequestingUserId { get; set; } = null!;
        public string RequestingUserName { get; set; } = null!;
        public string RequestingUserFirstLastName { get; set; } = null!;
        public string RequestingUserSecondLastName { get; set; } = null!;

        public string FullName =>
            $"{RequestingUserName} {RequestingUserFirstLastName} {RequestingUserSecondLastName}";

        public string AppointmentStatusName { get; set; } = null!;
    }
}