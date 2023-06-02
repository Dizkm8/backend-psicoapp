using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public class Client
    {
        #region CLASS_ATTRIBUTES
        [Key]
        public int Id { get; set; }
        public bool IsAdministrator { get; set; }
        #endregion


        #region ONE_TO_ONE_RELATIONSHIP
        public string? UserId { get; set; }
        public User User { get; set; } = null!;
        #endregion
    }
}