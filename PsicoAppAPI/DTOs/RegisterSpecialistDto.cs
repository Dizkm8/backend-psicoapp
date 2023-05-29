namespace PsicoAppAPI.DTOs
{
    public class RegisterSpecialistDto : BaseUserDto
    {
        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; } = string.Empty;
    }

}