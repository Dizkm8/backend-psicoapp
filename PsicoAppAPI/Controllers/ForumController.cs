using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Models;
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
    
    /// <summary>
    /// Get a forum post by its id in database context
    /// </summary>
    /// <param name="id">post id</param>
    /// <returns>Operation result</returns>
    [HttpGet("{id}")]
    public IActionResult GetForumPost(int id)
    {
        var post = _context.ForumPosts.FirstOrDefault(e => e.Id == id);
        if (post == default(ForumPost))
        {
            return NotFound();
        }
        return Ok(post);
    }
}