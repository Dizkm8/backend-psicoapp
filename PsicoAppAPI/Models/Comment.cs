using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models;

public class Comment
{
    #region CLASS_ATTRIBUTES
    
    [Key]
    public int Id { get; set; }
    public string? Content { get; set; }

    #endregion


    #region  MANY_TO_ONE_RELATIONSHIP
    public string? SpecialistId { get; set; }
    public User Specialist { get; set; } = null!;

    public int ForumPostId { get; set; }
    public ForumPost ForumPost { get; set; } = null!;

    #endregion
}