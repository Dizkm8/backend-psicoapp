namespace PsicoAppAPI.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //Relationships
        //1:N Specialist
         
        public List<Specialist> Specialists { get; set; } = new(); 

        
    }    
}