using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public class Role
    {
        #region CLASS_ATTRIBUTES
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        #endregion

        #region MODEL_RELATIONSHIPS

        #region ONE_TO_MANY_RELATIONSHIPS
        public List<User> Users { get;} = new();
        #endregion

        #endregion
    }
}