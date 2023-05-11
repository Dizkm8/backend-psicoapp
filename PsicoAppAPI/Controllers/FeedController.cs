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
    
    /// <summary>
    /// Add a feed post in database context 
    /// </summary>
    /// <param name="post">Post to add</param>
    /// <returns>Post saved</returns>
    [HttpPost]
    public IActionResult AddFeedPost(FeedPost post)
    {
        _context.FeedPosts.Add(post);
        _context.SaveChanges();
        return Ok(post);
    }

    /// <summary>
    /// Deletes a feed post given its id
    /// </summary>
    /// <param name="id">post id</param>
    /// <returns>Operation result</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteFeedPost(int id)
    {
        var deletedRows = _context.FeedPosts.Where(e => e.Id == id).ExecuteDelete();
        if (deletedRows <= 0)
        {
            return NotFound();
        }
        return Ok();
    }

    /// <summary>
    /// Get a feed post by id in database context
    /// </summary>
    /// <param name="id">post id</param>
    /// <returns>Operation result</returns>
    [HttpGet("{id}")]
    public IActionResult GetFeedPost(int id)
    {
        var post = _context.FeedPosts.FirstOrDefault(e => e.Id == id);
        if (post == default(FeedPost))
        {
            return NotFound();
        }
        return Ok(post);
    }
    
    /// <summary>
    /// Get all feed posts in database context
    /// </summary>
    /// <returns>All posts collected</returns>
    [HttpGet]
    public IActionResult GetFeedPosts()
    {
        var posts = _context.FeedPosts.ToList();
        return Ok(posts);
    }
}