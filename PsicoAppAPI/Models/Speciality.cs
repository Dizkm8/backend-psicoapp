using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


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