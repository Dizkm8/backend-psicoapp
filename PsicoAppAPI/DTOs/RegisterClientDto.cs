namespace PsicoAppAPI.DTOs
{
    public class RegisterClientDto : BaseUserDto
    {
        public bool IsAdministrator { get; set; }
    }
}