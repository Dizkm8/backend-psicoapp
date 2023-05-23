using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PsicoAppAPI.Models
{    public class Client : User{
        public bool IsAdministrator { get; set; }


        //Relationships

        //1:N FeedPost
        public List<FeedPost> FeedPosts { get; set; } = new();
        //1:N Appointment
        public List<Appointment> Appointment { get; set; } = new();

        
    }


    
}