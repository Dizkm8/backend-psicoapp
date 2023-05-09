using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models;

public class Appointment
{
    [Key]
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int Status { get; set; }
    public string? ClientId { get; set; }
    public string? SpecialistId { get; set; }

    public User Client { get; set; } = null!;
    public User Specialist { get; set; } = null!;
}