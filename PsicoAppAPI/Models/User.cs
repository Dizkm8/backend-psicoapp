using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public abstract class User
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

        #endregion
    }
}