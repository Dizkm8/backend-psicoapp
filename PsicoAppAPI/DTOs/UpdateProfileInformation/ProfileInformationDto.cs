namespace PsicoAppAPI.DTOs
{
    public class ProfileInformationDto : UpdateProfileInformationDto
    {
        public string Id { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}