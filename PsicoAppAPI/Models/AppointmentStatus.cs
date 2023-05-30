using System.ComponentModel.DataAnnotations;
namespace PsicoAppAPI.Models
{
    public class AppointmentStatus
    {
        #region CLASS_ATTRIBUTES    
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        #endregion


        #region ONE_TO_MANY_RELATIONSHIPS

        public List<Appointment> Appointment { get; set; } = new();

        #endregion
    }
}