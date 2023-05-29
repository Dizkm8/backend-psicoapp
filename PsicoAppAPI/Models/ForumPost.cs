namespace PsicoAppAPI.Models
{
    public class ForumPost
    {
        #region CLASS_ATTRIBUTES
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateOnly? PublishedOn { get; set; }
        public string? Tag { get; set; }
        public bool IsApproved { get; set; }

        #endregion


        #region MODEL_RELATIONSHIPS


        #region MANY_TO_ONE_RELATIONSHIP

        public string ClientId { get; set; } = null!;
        public Client Client { get; set; } = null!;

        #endregion


        #region ONE_TO_MANY_RELATIONSHIP

        public List<Comment> Comments { get; set; } = new();

        #endregion
        

        #endregion

    }
}