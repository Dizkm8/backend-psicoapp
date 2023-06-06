using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsicoAppAPI.Models
{
    public class AvailabilitySlot
    {
        #region CLASS_ATTRIBUTES
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [NotMapped]
        public bool IsAvailable
        {
            get
            {
                if (IsAvailableOverride && StartTime < DateTime.Now) return false;
                return IsAvailableOverride;
            }
        }
        // Ensure that IsAvailableOverride is true when AvailabilitySlot is created
        public bool IsAvailableOverride { get; set; } = true;
        #endregion

        #region MODEL_RELATIONSHIPS

        #region ONE_TO_MANY_RELATIONSHIPS
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        #endregion

        #endregion
    }
}