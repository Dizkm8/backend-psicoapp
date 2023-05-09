using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models;

public class Comment
{
     [Key]
     public int Id { get; set; }
     public string? Body { get; set; }
     public int PostId { get; set; }
     public int SpecialistId { get; set; }
     public string? SpecialistName { get; set; }
}