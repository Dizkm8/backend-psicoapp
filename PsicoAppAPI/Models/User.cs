using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PsicoAppAPI.Models
{
    

    public abstract class User
    {
        public string? Name { get; set; }
        public string? FirstLastName { get; set; }
        public string? SecondLastName { get; set; }
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public bool IsEnabled { get; set; }
        public int Phone { get; set; }
        public string? Password { get; set; }

        


    }
}
//test