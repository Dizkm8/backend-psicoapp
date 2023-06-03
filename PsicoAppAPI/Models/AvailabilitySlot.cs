using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public class AvailabilitySlot
    {
        #region CLASS_ATTRIBUTES
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }
        #endregion
        
        #region MODEL_RELATIONSHIPS
        
        #region ONE_TO_MANY_RELATIONSHIPS
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        #endregion

        #endregion
    }
}