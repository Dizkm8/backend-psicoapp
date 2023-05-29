namespace PsicoAppAPI.Models
{
    public class Specialist : User
    {
        //relationships

        //N:1 Speciality
        public int SpecialityId{ get; set; }

        public string SpecialityName { get; set; } = string.Empty;
        public Speciality Speciality{get;} = null!;

        //1:N FeedPost

        public List<FeedPost> FeedPosts { get; set; } = new();

        //1:N FeedPost

        public List<Appointment> Appointment { get; set; } = new();

        //1:N FeedPost

        public List<Comment> Comments { get; set; } = new();

        

        

        



       

    }

    
}