using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsicoAppAPI.DTOs
{
    public class RegisterClientDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? FirstLastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public int Phone { get; set; }

    }
}