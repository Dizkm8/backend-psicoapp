using PsicoAppAPI.Models;

namespace PsicoAppAPI.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime BookedDate { get; set; }
        public string RequestingUserId { get; set; } = null!;
        public string RequestingUserIdName { get; set; } = null!;
        public string RequestingUserIdFirstLastName { get; set; } = null!;
        public string RequestingUserIdSecondLastName { get; set; } = null!;

        public string FullName =>
            $"{RequestingUserIdName} {RequestingUserIdFirstLastName} {RequestingUserIdSecondLastName}";

        public AppointmentStatusDto AppointmentStatusDto { get; set; } = null!;
    }
}