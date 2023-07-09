using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PsicoAppAPI.Models
{
    public class GptRules
    {
        public int Id { get; set; }
        public string Rules { get; set; } = null!;
    }
}