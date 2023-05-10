using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
namespace PsicoAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedController : ControllerBase
{
    private readonly DataContext _context;
    
    public FeedController(DataContext context) => _context = context;
}