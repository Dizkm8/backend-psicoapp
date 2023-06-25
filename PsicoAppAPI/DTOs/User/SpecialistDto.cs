namespace PsicoAppAPI.DTOs.User;

public class SpecialistDto
{
    public string SpecialityName { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string UserFirstLastName { get; set; } = null!;
    public string UserSecondLastName { get; set; } = null!;
    public string UserFullName => $"{UserName} {UserFirstLastName} {UserSecondLastName}";
    public string UserEmail { get; set; } = null!;
    public string UserGender { get; set; } = null!;
    public bool UserIsEnabled { get; set; }
    public int UserPhone { get; set; }
    public string UserRoleName { get; set; } = null!;
}