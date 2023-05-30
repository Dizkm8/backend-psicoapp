namespace PsicoAppAPI.Models
{
    public class Speciality
    {
        #region CLASS_ATTRIBUTES
        
        public int Id { get; set; }
        public string? Name { get; set; }

        #endregion

        
        #region ONE_TO_MANY_RELATIONSHIPS

        public List<Specialist> Specialists { get; set; } = new(); 

        #endregion
        
    }    
}