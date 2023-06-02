using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Models;
using PsicoAppAPI.Data;

namespace PsicoAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
//     private readonly DataContext _context;

//     public CommentsController(DataContext context) => _context = context;
// /// <summary>
//     /// Add a forum post comment in database context 
//     /// </summary>
//     /// <param name="comment">Comment to add</param>
//     /// <returns>Comment saved</returns>
//     [HttpPost]
//     public IActionResult AddPostComment(Comment comment)
//     {
//         _context.Comments.Add(comment);
//         _context.SaveChanges();
//         return Ok(comment);
//     }
    
//     /// <summary>
//     /// Deletes a forum post comment given its id
//     /// </summary>
//     /// <param name="id">comment id</param>
//     /// <returns>Operation result</returns>
//     [HttpDelete("{id}")]
//     public IActionResult DeletePostComment(int id)
//     {
//         var comment = _context.Comments.FirstOrDefault(e => e.Id == id);
//         if (comment == default(Comment))
//         {
//             return NotFound();
//         }
//         _context.Comments.Remove(comment);
        
//         _context.SaveChanges();
//         return Ok();
//     }

//     // TODO: [AN-111] Añadir UpdatePostComment
    
//     /// <summary>
//     /// Get a forum post comment by id in database context
//     /// </summary>
//     /// <param name="id">comment id</param>
//     /// <returns>Operation result</returns>
//     [HttpGet("{id}")]
//     public IActionResult GetPostComment(int id)
//     {
//         var comment = _context.Comments.FirstOrDefault(e => e.Id == id);
//         if (comment == default(Comment))
//         {
//             return NotFound();
//         }
//         return Ok(comment);
//     }
    
//     /// <summary>
//     /// Get all comments in database context
//     /// </summary>
//     /// <returns>All posts collected</returns>
//     [HttpGet]
//     public IActionResult GetPostComments()
//     {
//         var posts = _context.Comments.ToList();
//         return Ok(posts);
//     }
    
//     /// <summary>
//     /// Get all comments of a forum post in database context
//     /// </summary>
//     /// <param name="postId">post id</param>
//     /// <returns>All comments collected</returns>
//     [HttpGet("forum/{postId}")]
//     public IActionResult GetPostCommentsByForum(int postId)
//     {
//         var comments = _context.Comments
//             .Where(e => e.PostId == postId)
//             .ToList();
//         return Ok(comments);
//     }
}