namespace PsicoAppAPI.DTOs.Appointment;

public class ClientAppointmentDto : AppointmentDto
{
    public string RequestedUserId { get; set; } = null!;
    public string RequestedUserName { get; set; } = null!;
    public string RequestedUserFirstLastName { get; set; } = null!;
    public string RequestedUserSecondLastName { get; set; } = null!;

    public string RequestedUserFullName =>
        $"{RequestedUserName} {RequestedUserFirstLastName} {RequestedUserSecondLastName}";
}