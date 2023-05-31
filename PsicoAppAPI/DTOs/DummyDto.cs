using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PsicoAppAPI.DTOs
{
    public class DummyDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string? Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
    }
}