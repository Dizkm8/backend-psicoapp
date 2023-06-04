using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public class Tag
    {
        #region CLASS_ATTRIBUTES
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        #endregion

        #region MODEL_RELATIONSHIPS

        #region ONE_TO_MANY_RELATIONSHIPS
        public List<FeedPost> FeedPosts { get; } = new();
        public List<ForumPost> ForumPosts { get;} = new();
        #endregion

        #endregion
    }
}