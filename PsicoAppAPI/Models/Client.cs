namespace PsicoAppAPI.Models
{
    public class Client : User
    {
        #region CLASS_ATTRIBUTES
        
        public bool IsAdministrator { get; set; }

        #endregion


        #region ONE_TO_MANY_RELATIONSHIPS
        
        public List<FeedPost> FeedPosts { get; } = new();
        public List<Appointment> Appointment { get; } = new();

        #endregion
    }
}