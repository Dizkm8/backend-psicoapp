namespace PsicoAppAPI.Models
{
    public class FeedPost
    {
        #region CLASS_ATTRIBUTES

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateOnly? PublishedOn { get; set; }
        public string? Tag { get; set; }

        #endregion
        

        #region MANY_TO_ONE_RELATIONSHIP
        public string? UserId { get; set; }
        public User User { get; set; } = null!;

        #endregion
    }
}