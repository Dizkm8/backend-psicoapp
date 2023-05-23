using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models;

public class Comment
{
     [Key]
     public int Id { get; set; }
     public string? Body { get; set; }
     public int PostId { get; set; }
     

     //Relationships

     //N:1 Specialist
     public int SpecialistId { get; set; }
     public string? SpecialistName { get; set; }
     public Specialist specialist{get;set;} = null!;

     //N:1 ForumPost
     public int ForumPostId{get; set;} 
     public int ForumPostTitle{get; set;} 

     public ForumPost ForumPost{get; set;} = null!;
}