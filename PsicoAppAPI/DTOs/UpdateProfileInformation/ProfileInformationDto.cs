namespace PsicoAppAPI.DTOs.UpdateProfileInformation
{
    public class ProfileInformationDto : UpdateProfileInformationDto
    {
        public string Id { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}