namespace PsicoAppAPI.DTOs
{
    public abstract class BaseUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FirstLastName { get; set; } = string.Empty;
        public string SecondLastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Phone { get; set; }
    }
}