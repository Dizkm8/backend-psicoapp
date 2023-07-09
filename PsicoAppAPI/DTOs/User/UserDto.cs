namespace PsicoAppAPI.DTOs.User;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string FirstLastName { get; set; } = null!;
    public string SecondLastName { get; set; } = null!;
    public string FullName => $"{Name} {FirstLastName} {SecondLastName}";

    public string Email { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public string Phone { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}