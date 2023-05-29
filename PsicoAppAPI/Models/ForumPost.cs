namespace PsicoAppAPI.Models
{
    public class ForumPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateOnly? PublishedOn { get; set; }
        public string? Tag { get; set; }
        public bool IsApproved { get; set; }


        //Relationships
        //N:1 Client
         public string? ClientId { get; set; }
         public string? ClientName { get; set; }
         public Client Client { get; set; } = null!;

        //1:N Comments
          public List<Comment> Comments { get; set; } = new();


        
    }
}