using System.ComponentModel.DataAnnotations;

namespace PsicoAppAPI.Models;

public class Comment
{
    #region CLASS_ATTRIBUTES
    [Key]
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime PublishedOn { get; set; } = DateTime.MinValue;
    #endregion

    #region MODEL_RELATIONSHIPS

    #region  MANY_TO_ONE_RELATIONSHIP
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public int ForumPostId { get; set; }
    public ForumPost ForumPost { get; set; } = null!;
    #endregion

    #endregion
}