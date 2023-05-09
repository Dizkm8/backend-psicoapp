using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PsicoAppAPI.Models
{
    public enum UserType {
        Admin,
        Client,
        Psychologist
    }

    public class User
    {
        public string? Name { get; set; }
        public string? FirstLastName { get; set; }
        public string? SecondLastName { get; set; }
        [Key]
        public string? RUT { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public bool IsEnabled { get; set; }
        public int Phone { get; set; }
        public string? Password { get; set; }
        public UserType Type { get; set; }

        // public List<FeedPost> FeedPosts { get; set; } = new();

    }
}