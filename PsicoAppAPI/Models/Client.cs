namespace PsicoAppAPI.Models
{
    public class Client : User
    {
        public bool IsAdministrator { get; set; }

        #region MODEL_RELATIONSHIPS
        public List<FeedPost> FeedPosts { get; set; } = new();
        public List<Appointment> Appointment { get; set; } = new();
        #endregion
    }
}