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
        if (!validateContent) return BadRequest(
            new ErrorModel { ErrorCode = 400, Message = "The title or content don't follow the rules to post" });

        var existsTag = await _service.CheckPostTag(addForumPost);
        if (!existsTag) return NotFound($"Tag with ID {addForumPost.TagId} does not exist");

        var postToReturn = await _service.AddForumPost(addForumPost);
        if (postToReturn is null) return StatusCode(StatusCodes.Status500InternalServerError,
            new ErrorModel { ErrorCode = 500, Message = "Internal error creating forum post" });
        return Ok(postToReturn);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ForumPostDto>>> GetAllForumPosts()
    {
        var posts = await _service.GetAllPosts();
        if(posts is null) return StatusCode(StatusCodes.Status500InternalServerError,
            new ErrorModel { ErrorCode = 500, Message = "Internal error fetching all forum posts" });
        return Ok(posts);
    }
}