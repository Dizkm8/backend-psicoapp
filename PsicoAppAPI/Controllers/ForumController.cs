using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Data;
namespace PsicoAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForumController : ControllerBase
{
    private readonly DataContext _context;

    public ForumController(DataContext context) => _context = context;

    /// <summary>
    /// Add a forum post in database context 
    /// </summary>
    /// <param name="post">Post to add</param>
    /// <returns>Post saved</returns>
    [HttpPost]
    public IActionResult AddForumPost(ForumPost post)
    {
        _context.ForumPosts.Add(post);
        _context.SaveChanges();
        return Ok(post);
    }
}