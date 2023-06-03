using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public class User
    {
        #region CLASS_ATTRIBUTES
        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FirstLastName { get; set; } = null!;
        public string SecondLastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public int Phone { get; set; }
        public string HashedPassword { get; set; } = null!;
        #endregion

        #region MODEL_RELATIONSHIPS

        #region MANY_TO_ONE_RELATIONSHIPS
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        #endregion

        #region ONE_TO_MANY_RELATIONSHIPS
        public List<FeedPost> FeedPosts { get; } = new();
        public List<ForumPost> ForumPosts { get; } = new();
        #endregion

        #endregion
    }
}