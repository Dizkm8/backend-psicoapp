namespace PsicoAppAPI.DTOs.User;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string FirstLastName { get; set; } = null!;
    public string SecondLastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public int Phone { get; set; }
    public string Password { get; set; } = null!;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
    public int SpecialityId { get; set; }
    public string SpecialityName { get; set; } = null!;
}