namespace PsicoAppAPI.Models
{
    public class ForumPost
    {
        #region CLASS_ATTRIBUTES
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateOnly PublishedOn { get; set; } = DateOnly.MinValue;
        #endregion

        #region MODEL_RELATIONSHIPS

        #region MANY_TO_ONE_RELATIONSHIP
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
        #endregion

        #region ONE_TO_MANY_RELATIONSHIP
        public List<Comment> Comments { get; } = new();
        #endregion

        #endregion

    }
}