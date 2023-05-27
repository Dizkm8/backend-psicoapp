using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PsicoAppAPI.Models
{
     public class Specialist : User
    {
        //relationships

        //N:1 Speciality
        public int SpecialityId{ get; set; }

        public int SpecialityName { get; set; }
        public Speciality Speciality{get;} = null!;

        //1:N FeedPost

        public List<FeedPost> FeedPosts { get; set; } = new();

        //1:N FeedPost

        public List<Appointment> Appointment { get; set; } = new();

        //1:N FeedPost

        public List<Comment> Comments { get; set; } = new();

       

    }

    
}