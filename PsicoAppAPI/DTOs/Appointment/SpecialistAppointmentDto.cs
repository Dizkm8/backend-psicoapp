namespace PsicoAppAPI.DTOs.Appointment;

public class SpecialistAppointmentDto : AppointmentDto
{
    public string RequestingUserId { get; set; } = null!;
    public string RequestingUserName { get; set; } = null!;
    public string RequestingUserFirstLastName { get; set; } = null!;
    public string RequestingUserSecondLastName { get; set; } = null!;

    public string RequestingUserFullName =>
        $"{RequestingUserName} {RequestingUserFirstLastName} {RequestingUserSecondLastName}";
}