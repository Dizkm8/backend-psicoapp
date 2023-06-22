namespace PsicoAppAPI.DTOs.ForumPost;

public class ForumPostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateOnly PublishedOn { get; set; } = DateOnly.MinValue;
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string UserFirstLastName { get; set; } = null!;
    public string UserSecondLastName { get; set; } = null!;
    public string FullName
    {
        get
        {
            return $"{UserName} {UserFirstLastName} {UserSecondLastName}";
        }
    }
    public string TagName { get; set; } = null!;
    public List<CommentDto> Comments { get; set; } = new();
}