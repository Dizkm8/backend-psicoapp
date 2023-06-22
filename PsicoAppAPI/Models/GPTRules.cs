using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models
{
    public class GPTRules
    {
        [Key]
        public int Id { get; set; }
        public string Rules { get; set; } = null!;
    }
}