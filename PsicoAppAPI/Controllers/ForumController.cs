using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Data;
namespace PsicoAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForumController : ControllerBase
{
    private readonly DataContext _context;

    public ForumController(DataContext context) => _context = context;
}