using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public class User
    {
        #region CLASS_ATTRIBUTES

        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? FirstLastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public bool IsEnabled { get; set; }
        public int Phone { get; set; }
        public string? Password { get; set; }

        #region ONE_TO_MANY_RELATIONSHIPS

        public List<FeedPost> FeedPosts { get; } = new();
        public List<Appointment> Appointment { get; } = new();

        #endregion

        #endregion
    }
}