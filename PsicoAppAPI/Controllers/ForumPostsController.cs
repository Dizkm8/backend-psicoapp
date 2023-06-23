using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsicoAppAPI.Controllers.Base;
using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.ForumPost;
using PsicoAppAPI.Mediators.Interfaces;

namespace PsicoAppAPI.Controllers;

public class ForumPostsController : BaseApiController
{
    private readonly IForumPostManagementService _service;

    public ForumPostsController(IForumPostManagementService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Create a new Forum post
    /// </summary>
    /// <param name="addForumPost">
    /// Title: Post's title, must be not null or empty
    /// Content: Post's content, must be not null or empty and less than 255 characters
    /// TagId: Post's tag id, must be not null and exist in the database
    /// </param>
    /// <returns>
    /// If the User Id from the provided token does not exists or is disabled
    /// If the user Id from the provided token doesn't match with a client return 500 Internal server error
    /// If the title or content are null or empty return 400 Bad request
    /// If the title or content are not adecuate to the application return 400 Bad request (GPT 3.5-turbo criteria)
    /// If something went wrong adding the post return 500 Internal server error
    /// If the tag id does not exist return 404 Not found
    /// </returns>
    [Authorize(Roles = "2")]
    [HttpPost("create-post")]
    public async Task<ActionResult> AddForumPost(AddForumPostDto addForumPost)
    {
        var validateContent = await _service.CheckPost(addForumPost);
        if (!validateContent)
            return BadRequest(
                new ErrorModel { ErrorCode = 400, Message = "The title or content don't follow the rules to post" });

        var existsTag = await _service.CheckPostTag(addForumPost);
        if (!existsTag) return NotFound($"Tag with ID {addForumPost.TagId} does not exist");

        var postToReturn = await _service.AddForumPost(addForumPost);
        if (postToReturn is null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorModel { ErrorCode = 500, Message = "Internal error creating forum post" });
        return Ok(postToReturn);
    }

    /// <summary>
    /// Get all forum posts
    /// </summary>
    /// <returns>
    /// If the user Id from the provided token doesn't match with a client return 500 Internal server error
    /// If something went wrong fetching the posts return 500 Internal server error
    /// If everything goes well, return a List with the following properties:
    /// Id: Post's id
    /// Title: Post's title
    /// Content: Post's content
    /// PublishedOn: Post's published date (ISO 8601)
    /// UserId: Post's user id
    /// 
    /// The next three attributes are used to show the user's full name
    /// I suggest threat like "private" stuff, so they are not used in the client side
    /// use fullName attribute instead
    /// UserName: Post's user name 
    /// UserFirstLastName: Post's user first last name
    /// UserSecondLastName: Post's user second last name
    /// 
    /// FullName: Post's user full name
    /// TagName: Post's tag name
    /// Comments: Post's comments, this is a list, so, follow this structure:
    ///    Id: Comment's id
    ///    Content: Comment's content
    ///    PublishedOn: Comment's published date (ISO 8601)
    ///    
    ///    As the previous attributes, the next three attributes are used to show the user's full name
    ///    same rules apply here
    ///    UserName: Comment's user name
    ///    UserFirstLastName: Comment's user first last name
    ///    UserSecondLastName: Comment's user second last name
    /// 
    ///    FullName: Comment's user full name
    /// </returns>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ForumPostDto>>> GetAllForumPosts()
    {
        var posts = await _service.GetAllPosts();
        if (posts is null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorModel { ErrorCode = 500, Message = "Internal error fetching all forum posts" });
        return Ok(posts);
    }

    [Authorize(Roles = "3")]
    [HttpPost("add-comment/{postId:int}")]
    public async Task<ActionResult> CommentForumPost([Required] int postId,
        [Required] [FromBody] string content)
    {
        var isSpecialist = await _service.IsUserSpecialist();
        if (!isSpecialist) return Unauthorized("The user with userId from token are not a valid specialist");

        var existsPost = await _service.ExistsPost(postId);
        if (!existsPost) return BadRequest("Post Id do not match with any existing post");

        var result = await _service.AddComment(postId, content);
        if(!result) return StatusCode(StatusCodes.Status500InternalServerError,
            new ErrorModel { ErrorCode = 500, Message = "Internal error adding a new comment" });
        return Ok();
    }
    
    
}