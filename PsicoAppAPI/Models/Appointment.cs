using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models;

public class Appointment
{
    [Key]
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int Status { get; set; }
    

    //Relationships


    //N:1 Client
    public string? ClientId { get; set; }
    public string? ClientName { get; set; }
    public Client Client { get; set; } = null!;

     //N:1 Specialist
    public string? specialisttId { get; set; }
    public string? specialistName { get; set; }  
    public Specialist Specialist { get; set; } = null!;
}