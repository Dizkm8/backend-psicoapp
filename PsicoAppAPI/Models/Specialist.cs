using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public class Specialist
    {
        #region CLASS_ATTRIBUTES
        [Key]
        public int Id { get; set; }
        #endregion
        

        #region MODEL_RELATIONSHIPS


        #region ONE_TO_ONE_RELATIONSHIP
        public string? UserId { get; set; }
        public User User { get; set; } = null!;
        #endregion


        #region MANY_TO_ONE_RELATIONSHIP
        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; } = null!;
        #endregion

        #endregion
    }
}