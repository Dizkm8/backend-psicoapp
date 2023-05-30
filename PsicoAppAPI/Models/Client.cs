namespace PsicoAppAPI.Models
{
    public class Client
    {
        #region CLASS_ATTRIBUTES
        public bool IsAdministrator { get; set; }
        #endregion


        #region ONE_TO_ONE_RELATIONSHIP
        public string? UserId { get; set; }
        public User User { get; set; } = null!;
        #endregion
    }
}