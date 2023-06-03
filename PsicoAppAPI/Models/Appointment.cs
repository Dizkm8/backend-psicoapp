using System.ComponentModel.DataAnnotations;
namespace PsicoAppAPI.Models;

public class Appointment
{
    #region CLASS_ATTRIBUTES
    [Key]
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    #endregion

    #region MODEL_RELATIONSHIPS

    #region ONE_TO_MANY_RELATIONSHIPS
    public string RequestingUserId { get; set; } = null!;
    public User RequestingUser { get; set; } = null!;

    public string RequestedUserId { get; set; } = null!;
    public User RequestedUser { get; set; } = null!;

    public int AppointmentStatusId { get; set; }
    public AppointmentStatus AppointmentStatus { get; set; } = null!;
    #endregion

    #endregion
}
