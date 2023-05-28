using System.ComponentModel.DataAnnotations;
namespace PsicoAppAPI.Models
{    public class AppointmentStatus{
        [Key]
        public int Id { get; set; }

        public string? Name{ get; set; }


        //Relationships

        //1:N Appointment
        public List<Appointment> Appointment { get; set; } = new();
       

        
    }


    
}