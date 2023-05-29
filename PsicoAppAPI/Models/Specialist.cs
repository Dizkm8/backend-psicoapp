namespace PsicoAppAPI.Models
{
    public class Specialist : User
    {
        #region MODEL_RELATIONSHIPS


        #region MANY_TO_ONE_RELATIONSHIP
        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; } = null!;

        #endregion


        #region ONE_TO_MANY_RELATIONSHIPS

        public List<FeedPost> FeedPosts { get; } = new();
        public List<Appointment> Appointment { get; } = new();
        public List<Comment> Comments { get; } = new();

        #endregion


        #endregion
    }
}