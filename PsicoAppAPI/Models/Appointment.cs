using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models;

public class Appointment
{
    [Key]
    public int Id { get; set; }
    public DateTime AppointmentTime { get; set; }
    public int Status { get; set; }
    public string? ClientId { get; set; }
    public string? SpecialistId { get; set; }
}