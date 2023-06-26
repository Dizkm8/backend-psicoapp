namespace PsicoAppAPI.DTOs.Appointment;

public class AppointmentStatisticsDto
{
    public int CanceledAppointmentQuantity { get; set; }
    public int BookedAppointmentQuantity { get; set; }
    public int DoneAppointmentQuantity { get; set; }
}