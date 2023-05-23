using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// TODO: [AN-112] Aplicar herencia para FeedPost y ForumPost (o fusionarlas)
namespace PsicoAppAPI.Models
{
    public class FeedPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateOnly? OnPublished { get; set; }
        public string? Tag { get; set; }

        //Relationships
        //N:1 Specialist
        public string? SpecialistId { get; set; }
        public string? SpecialistName { get; set; }
        public Specialist Specialist { get; set; } = null!;
    }
}