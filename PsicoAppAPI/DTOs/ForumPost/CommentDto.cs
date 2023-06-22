namespace PsicoAppAPI.DTOs.ForumPost
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
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
        public DateTime PublishedOn { get; set; } = DateTime.MinValue;
    }
}