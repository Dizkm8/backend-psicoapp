namespace PsicoAppAPI.Models
{    public class HoursAvailable{
        public TimeOnly AvailableTime { get; set; }

        public bool IsBooked { get; set; }


        //Relationships

        //N:1 Specialist
        public string? SpecialistId { get; set; }
       

        
    }


    
}