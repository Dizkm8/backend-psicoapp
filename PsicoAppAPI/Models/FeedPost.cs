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
        public string? SpecialistId { get; set; }
        public User Specialist { get; set; } = null!;

        #endregion
    }
}