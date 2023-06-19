using System;

namespace PsicoAppAPI.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? SpecialistId { get; set; }
        public string? Status { get; set; }
        public DateTime BookedDate { get; set; }
    }
}
